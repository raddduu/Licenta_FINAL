import pyodbc
import pandas as pd
from transformers import BertModel, BertTokenizer

# Load the tokenizer
tokenizer = BertTokenizer.from_pretrained("./romanian_tokenizer")

# Load the saved model
model = BertModel.from_pretrained("./romanian_model")

def get_bert_embedding(text, model, tokenizer):
    inputs = tokenizer(text, return_tensors="pt")
    outputs = model(**inputs)
    return outputs[0].mean(dim=1).detach().numpy()

# Connection string
connection_string = (
    "DRIVER={ODBC Driver 17 for SQL Server};"
    "SERVER=(localdb)\\mssqllocaldb;"
    "DATABASE=aspnet-ManageMe-8b4978f1-bd53-4942-8e49-8f4eae22e6d1;"
    "Trusted_Connection=yes;"
    "MultipleActiveResultSets=true;"
)

# Establishing the connection
conn = pyodbc.connect(connection_string)
cursor = conn.cursor()


def get_table_data(table_name):
    cursor.execute(f"SELECT * FROM {table_name}")
    rows = cursor.fetchall()
    return rows

def build_dataframe(table_name):
    rows = get_table_data(table_name)
    
    df = pd.DataFrame()
    df['Id'] = [f"{table_name[:3]}_{row[0]}" for row in rows]
    df['Text'] = [row[1] for row in rows]
    df['Embedding'] = [get_bert_embedding(row[1], model, tokenizer) for row in rows]
    type_value = 'Stem' if table_name in ['Methodology', 'Chapter'] else 'Branch' if table_name == 'Section' else 'Leaf'
    df['Type'] = [type_value] * len(rows)
    
    if table_name == 'Paragraphs':
        df['Parent'] = [f"Sec_{row[2]}" for row in rows]
        
    if table_name == 'Details':
        df['Parent'] = [f"Par_{row[2]}" if row[2] else f"Det_{row[3]}" for row in rows]
        
    if table_name == 'Provisions':
        df['Parent'] = [f"Art_{row[2]}" if row[2] else f"Pro_{row[3]}" for row in rows]
        
    if table_name == 'Articles':
        df['Parent'] = [f"Cha_{row[2]}" for row in rows]
        
    if table_name == 'Sections':
        df['Parent'] = [f"Cha_{row[2]}" if row[2] else f"Sec_{row[3]}" for row in rows]
        
    if table_name == 'Chapters':
        df['Parent'] = [None] * len(rows)

    return df

def save_dataframe(df, table_name):
    df.to_pickle(f"{table_name}.pkl")

def process_table(table_name):
    df = build_dataframe(table_name)
    save_dataframe(df, table_name)

# Processing the tables
tables = ['Chapters', 'Sections', 'Articles', 'Paragraphs', 'Details', 'Provisions']

for table in tables:
    process_table(table)

# Closing the connection
conn.close()
