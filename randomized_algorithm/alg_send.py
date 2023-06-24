import requests
import json

# BASE = " http://127.0.0.1:5000/"


# def send_information_to_server(json_solution):
#     with open(f"{json_solution}", 'r') as json_file:
#         contents = json.loads(json_file.read())
#         print(contents)
#         response = requests.put(BASE + "solution/1", json=contents)
#         print(response.json())
#
#
# def send_to_server(json_file):
#     send_information_to_server(json_solution="output_construct.json")
#     print("----------------------------------------------------------------")
#     send_information_to_server(json_solution="output_improve.json")


BASE = "https://localhost:7165/"


def send_information_to_server(json_solution):
    contents = json.loads(json_solution.read())

    # response = requests.post(BASE + "solution/1", json=contents)
    response = requests.post(BASE + "api / Result / DeliveryArrangement", json=contents, verify=False)
    print(response.json())


def send_to_server(json_file):
    send_information_to_server(json_solution=json_file)
    # print("----------------------------------------------------------------")
    # send_information_to_server(json_solution="output_improve.json")
