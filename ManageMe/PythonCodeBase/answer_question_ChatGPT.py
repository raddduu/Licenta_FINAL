from openai import OpenAI
import os
import sys

# Read the API key
with open("api_key.txt", "r", encoding="utf-8-sig") as f:
    api_key = f.read().strip()

# Initialize the OpenAI client
client = OpenAI(api_key=api_key)

# Function to generate the prompt
def generate_prompt(question, context):
    prompt = f"""
        Acestea sunt informatiile pe care le am la dispozitie:

        "{context}"

        Am urmatoarea intrebare referitoare la aceste informatii:

        "{question}"

        Raspunsul la aceasta intrebare este:
    """
    return prompt

# Model parameters
MODEL = "gpt-3.5-turbo"
TEMPERATURE = 0.5
MAX_TOKENS = 4096
SYSTEM_MESSAGE = """
    You are a helpful assistant.
    You are here to analyze pieces of information representing
    fragaments of universitary methodologies and answer questions about them.
    You must be able to provide a clear and concise answer to the user's questions.
    The users are from Romania, so you should be able to understand and answer questions in Romanian.
"""

# Function to answer the question
def answer_question(question, context):
    prompt = generate_prompt(question, context)
    
    response = client.chat.completions.create(
        model=MODEL,
        messages=[
            {"role": "system", "content": SYSTEM_MESSAGE},
            {"role": "user", "content": prompt},
        ],
        temperature=TEMPERATURE,
    )
    return response.choices[0].message.content

if __name__ == "__main__":
    question = sys.argv[1]
    
    with open("output.txt", "r", encoding="utf-8-sig") as f:
        context = f.read()
    
    answer = answer_question(question, context)
    
    with open("output.txt", "w", encoding="utf-8") as f:
        f.write(answer)
