import sys
import json

def main(args):
    if len(args) < 1:
        print("No input data provided")
        return

    input_data = args[0]
    try:
        data = json.loads(input_data)
    except json.JSONDecodeError as e:
        print(f"Failed to decode JSON: {e}")
        return

    result = {"message": "Script executed successfully", "input_data": data}
    print(json.dumps(result))

if __name__ == "__main__":
    #main(sys.argv[1:])
    print(sys.argv[1])
