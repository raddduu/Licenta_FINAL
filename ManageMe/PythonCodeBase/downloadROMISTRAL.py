from transformers import AutoTokenizer, AutoModelForCausalLM

tokenizer = AutoTokenizer.from_pretrained("OpenLLM-Ro/RoMistral-7b-Instruct")
tokenizer.save_pretrained("./romanian_MISTRAL_tokenizer")

model = AutoModelForCausalLM.from_pretrained("OpenLLM-Ro/RoMistral-7b-Instruct")
model.save_pretrained("./romanian_MISTRAL_model")