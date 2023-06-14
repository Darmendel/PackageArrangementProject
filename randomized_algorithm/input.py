import pandas as pd
import numpy as np
from itertools import permutations
import package
from constants import *
import json
from package import Package


class Input:

    def __init__(self, file_name: str, file_path=None):  # file path might not be used.
        self.container = []  # format: Height, Width, Length.
        self.boxes = []
        self.categories = []
        self.data = pd.read_csv(file_name)
        self.df_container1 = self.data.iloc[:2, :]
        self.df_boxes = self.data.iloc[2:, :].values.tolist()
        self.boxes = self.df_boxes
        self.convert_to_float()
        self.contdim = np.array(
            [self.df_container1["Height"][0], self.df_container1["Width"][0], self.df_container1["Length"][0]])
        self.shipment_number = 1
        self.create_boxes()

    def convert_to_float(self):
        self.boxes = [[int(i[0]), int(i[1]), int(i[2]), int(i[3])] if idx else i for idx, i in
                      enumerate(self.boxes)]

    # box format: 'Customer ,Height', 'Width', 'Length', 'Value', 'Volume', 'Priority', 'Above', 'Position'
    def create_boxes(self):
        self.boxes[0][0] = "Customer_rank"
        self.val_volume("Value")
        self.add_priority()
        self.add_above()
        self.add_position()

    '''if no value defined then it is set to equal value 1 for all'''
    '''volume is defined as  Height * Width * Length'''

    def val_volume(self, arg):
        if arg not in self.boxes[0]:
            self.boxes[0].append(arg)

        for i in self.boxes[1:]:
            if arg == "Value":
                i.append(1.0)
            else:
                i.append(float(i[1]) * float(i[2]) * float(i[3]))

    '''if there is no priority, all will be given 10, meaning all items need to in a list'''

    def add_priority(self):
        if "Priority" not in self.boxes[0]:
            self.boxes[0].append("Priority")
            for i in self.boxes[1:]:
                i.append(10)

    '''if above for a box is not specified, the default is true.'''
    '''if item is fragile(no above) - 0, otherwise 1 '''

    def add_above(self):
        if "Above" not in self.boxes[0]:
            self.boxes[0].append("Above")
            for i in self.boxes[1:]:
                i.append(Above.ALLOWED)

    '''adds all possible position for each item, if no positions columns was found. '''

    def add_position(self):
        if "Positions" not in self.boxes[0]:
            self.boxes[0].append("Position")
            all_pos = [pos for pos in permutations(["Height", "Width", "Length"])]
            for i in self.boxes[1:]:
                i.append(all_pos)


# class InputJson:
#     def __init__(self, file_name):
#         with open(file_name, 'r') as json_file:
#             contents = json.loads(json_file.read())
#             container_dict = contents["container"]
#             self.contdim = np.array([container_dict["Height"],
#                                      container_dict["Width"
#                                      ], container_dict["Length"]])
#             self.shipment_number = container_dict["ShipmentNumber"]
#             self.pkgs = []
#             for index_, pkg in enumerate(contents["packages"]):
#                 self.pkgs.append(Package(customer=pkg["Unpack order"],
#                                          length=pkg["Length"],
#                                          width=pkg["Width"],
#                                          height=pkg["Height"],
#                                          unique_idx=pkg["Unique_Idx"],
#                                          index=index_
#                                          ))


class InputJson:
    def __init__(self, file_name):
        with open(file_name, 'r') as json_file:
            contents = json.loads(json_file.read())
            container_dict = contents["Container"]
            self.contdim = np.array([int(container_dict["Height"]),
                                     int(container_dict["Width"]),
                                     int(container_dict["Length"])
                                     ])
            self.shipment_number = int(contents["Id"])
            self.cost = int(container_dict["Cost"])
            self.pkgs = []
            for index_, pkg in enumerate(contents["Packages"]):
                self.pkgs.append(Package(customer=int(pkg["Order"]),
                                         length=int(pkg["Length"]),
                                         width=int(pkg["Width"]),
                                         height=int(pkg["Height"]),
                                         unique_idx=int(pkg["DeliveryId"]),
                                         index=index_
                                         ))
