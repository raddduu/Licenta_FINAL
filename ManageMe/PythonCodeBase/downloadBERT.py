from transformers import BertModel, BertTokenizer

tokenizer = BertTokenizer.from_pretrained("dumitrescustefan/bert-base-romanian-cased-v1")
tokenizer.save_pretrained("./romanian_tokenizer")

model = BertModel.from_pretrained("dumitrescustefan/bert-base-romanian-cased-v1")
model.save_pretrained("./romanian_model")