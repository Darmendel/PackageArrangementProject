from flask import Flask, request
from flask_restful import Api, Resource, reqparse

import json

app = Flask(__name__)
api = Api(app)


class ConstructedSolution(Resource):
    # def get(self):
    #     return contents
    def post(self, solution_num):
        # print(request.form)
        print(request.get_json())
        return {}


api.add_resource(ConstructedSolution, "/solution/<int:solution_num>")

if __name__ == "__main__":
    app.run(debug=True)  # erase this later.
