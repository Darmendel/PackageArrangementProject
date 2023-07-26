import requests
import json

# BASE = " http://127.0.0.1:5000/"  # for testing.


BASE = "https://localhost:7165/"


def send_information_to_server(json_solution):
    contents = json.loads(json_solution.read())

    # response = requests.post(BASE + "solution/1", json=contents)  # for testing
    response = requests.post(BASE + "api/Result/DeliveryArrangement", json=contents, verify=False)
    # print(response.json())  # for testing


def send_to_server(json_file):
    send_information_to_server(json_solution=json_file)

