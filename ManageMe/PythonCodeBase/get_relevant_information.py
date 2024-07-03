import sys
from sklearn.metrics.pairwise import cosine_similarity
from transformers import BertModel, BertTokenizer
import pandas as pd
import numpy as np
import json
from timeit import timeit
import matplotlib.pyplot as plt

class Node:
    def __init__(self, id, text, parentNodeId, isRoot=False):
        self.id = id
        self.text = text
        self.parentNodeId = parentNodeId
        self.isRoot = isRoot
        self.childrenNodes = []

def get_bert_embedding(text, model, tokenizer, max_length=512):
    inputs = tokenizer(text, return_tensors="pt", padding=True, truncation=True)
    outputs = model(**inputs)
    return outputs.last_hidden_state.mean(dim=1).detach().numpy()

def get_tree(question_text):
    tokenizer = BertTokenizer.from_pretrained("./romanian_tokenizer")
    model = BertModel.from_pretrained("./romanian_model")

    articles_df = pd.read_pickle("Articles.pkl")
    chapters_df = pd.read_pickle("Chapters.pkl")
    details_df = pd.read_pickle("Details.pkl")
    methodologies_df = pd.read_pickle("Methodology.pkl")
    paragraphs_df = pd.read_pickle("Paragraphs.pkl")
    provisions_df = pd.read_pickle("Provisions.pkl")
    sections_df = pd.read_pickle("Sections.pkl")

    df = pd.concat([articles_df, chapters_df, details_df, methodologies_df, paragraphs_df, provisions_df, sections_df])
    df.loc[df['Id'].str.startswith('Cha'), 'Parent'] = None
    df['Parent'] = df['Parent'].fillna(-1)

    question_embedding = get_bert_embedding(question_text, model, tokenizer)

    def compute_similarity(embedding):
        embedding = np.array(embedding)  
        embedding = embedding.reshape(1, -1) if embedding.ndim == 1 else embedding  
        return cosine_similarity(question_embedding, embedding)[0][0]

    df['Embedding'] = df['Text'].apply(lambda x: get_bert_embedding(x, model, tokenizer))
    df['Similarity'] = df['Embedding'].apply(compute_similarity)
    df = df.sort_values(by='Similarity', ascending=False)

    nodes = []
    id_to_index = {}
    for i, row in enumerate(df.iterrows()):
        node = Node(row[1]['Id'], row[1]['Text'], row[1]['Parent'], row[1]['Parent'] == -1)
        nodes.append(node)
        id_to_index[node.id] = i

    return nodes, id_to_index

def tree_search(node_id, previous_node_id, df_nodes, id_to_index, selected_nodes_ids, selected_nodes):
    node_index = id_to_index[node_id]
    if previous_node_id is not None:
        previous_node_index = id_to_index[previous_node_id]
        df_nodes[node_index].childrenNodes.append(df_nodes[previous_node_index]) 

    if node_id not in selected_nodes_ids and df_nodes[node_index].parentNodeId != -1:
        selected_nodes_ids.add(node_id)
        selected_nodes.append(df_nodes[node_index])
        parent_id = df_nodes[node_index].parentNodeId
        tree_search(parent_id, node_id, df_nodes, id_to_index, selected_nodes_ids, selected_nodes)

def get_text(node, level, result_text):
    indent = "  " * level
    result_text.append(indent + node.text)
    for child in node.childrenNodes:
        get_text(child, level + 1, result_text)

def get_knowledge_base(question, top_nodes):
    df_nodes, id_to_index = get_tree(question)
    seed_nodes = df_nodes[:top_nodes]

    selected_nodes_ids = set()
    selected_nodes = []

    for node in seed_nodes:
        tree_search(node.id, None, df_nodes, id_to_index, selected_nodes_ids, selected_nodes)

    for seed_node in seed_nodes:
        if seed_node.id not in selected_nodes_ids:
            selected_nodes.append(seed_node)
            selected_nodes_ids.add(seed_node.id)

    nodeIds = [node.id for node in selected_nodes]

    for selected_node in selected_nodes:
        children = selected_node.childrenNodes
        for child in children:
            if child.id in nodeIds:
                nodeIds.remove(child.id)

    knowledge_base = []

    for nodeId in nodeIds:
        for node in selected_nodes:
            if node.id == nodeId:
                result_text = []
                get_text(node, 0, result_text)
                branch_info = "\n".join(result_text)
                knowledge_base.append(branch_info)

    final_text = "\n".join(knowledge_base)

    with open("output.txt", "w", encoding='utf-8') as f:
        f.write(final_text)

if __name__ == "__main__":
    question = sys.argv[1]

    top_value = 10

    get_knowledge_base(question, top_value)