# TODO change the weight limit to something reasonable.
import json
import numpy as np
from package import Package
import copy
from constants import *
import io

BASE_RATIO = 0.8


# def list_dict_packages(pkgs: list[Package]) -> list[dict]:
#     pkgs_json = []
#     for pkg in pkgs:
#         pack_item = {
#             "Width": pkg.width,
#             "Length": pkg.length,
#             "Height": pkg.height,
#             "Unique_Idx": pkg.unique_idx
#         }
#         pkgs_json.append(pack_item)
#     return pkgs_json


class Container:

    def list_dict_packages(self, pkgs: list[Package]) -> list[dict]:
        pkgs_json = []
        for pkg in pkgs:
            pack_item = {
                "Id": str(pkg.unique_idx),
                "Width": str(pkg.width),
                "Depth": str(pkg.length),
                "Height": str(pkg.height),
                "Order": str(pkg.customer),
                "X": str(pkg.location[0]),
                "Y": str(pkg.location[1]),
                "Z": str(pkg.location[2]),

            }
            pkgs_json.append(pack_item)
        return pkgs_json

    def __init__(self, height: int, width: int, length: int, pkgs_num: int, shipment_number: int = 0,
                 weight_limit: int = 5000, cost: int = 0, size: int = 1, userid: int = 0):
        self.height = height
        self.width = width
        self.length = length
        self.weight_limit = weight_limit
        self.volume = self.height * self.width * self.length
        # self.pkgs_num = pkgs_num
        self.shipment_number = shipment_number
        self.pkgs_construct = None
        self.pkgs_improve = None
        self.taken_space = np.zeros(shape=(self.length, self.width, self.height), dtype=float)
        # self.taken_space[0: self.length, 0: self.width, 0:1] = Used.YES.value  # basis
        self.cost = cost
        self.size = size
        self.userid = userid

    # update weight & occupied space.
    def update_taken_space(self, pkg: Package) -> None:
        self.taken_space[pkg.location[0]: pkg.location[0] + pkg.length,
        pkg.location[1]: pkg.location[1] + pkg.width,
        pkg.location[2]: pkg.location[2] + pkg.height] = Used.YES.value

    def erase_taken_space(self, pkg: Package) -> None:
        self.taken_space[pkg.location[0]: pkg.location[0] + pkg.length,
        pkg.location[1]: pkg.location[1] + pkg.width,
        pkg.location[2]: pkg.location[2] + pkg.height] = Used.NO.value

    # def check_base(self, pkg: Package, pos: Location) -> bool:
    #     used_base = np.count_nonzero(self.taken_space[pos[0]: pos[0] + pkg.length + 1,
    #                                  pos[1]: pos[1] + pkg.width + 1,
    #                                  pos[2]: pos[2] + 1] == Used.YES.value)
    #
    #     return True if used_base / (pkg.length * pkg.width) > BASE_RATIO else False

    def move_along_x_axis(self, pkg: Package) -> None:
        i = 0
        self.erase_taken_space(pkg=pkg)
        while (pkg.location[0] - i > 0 and np.all(
                self.taken_space[pkg.location[0] - i - 1: pkg.location[0] - i,
                pkg.location[1]: pkg.location[1] + pkg.width,
                pkg.location[2]: pkg.location[2] + pkg.height] == Used.NO.value)):
            i += 1
        i -= 0
        if i > 0:
            print(f"check x axis {i}")

        t = list(pkg.location)
        t[0] -= i
        pkg.location = tuple(t)
        self.update_taken_space(pkg=pkg)  # new space occupied

    def move_along_y_axis(self, pkg: Package) -> None:
        i = 0
        self.erase_taken_space(pkg=pkg)
        while (pkg.location[1] - i > 0 and np.all(
                self.taken_space[pkg.location[0]: pkg.location[0] + pkg.length,
                pkg.location[1] - i - 1: pkg.location[1] - i,
                pkg.location[2]: pkg.location[2] + pkg.height] == Used.NO.value)):
            i += 1
        i -= 0
        if i > 0:
            print(f"check y axis {i}")

        t = list(pkg.location)
        t[1] -= i
        pkg.location = tuple(t)
        self.update_taken_space(pkg=pkg)  # new space occupied

    def move_along_z_axis(self, pkg: Package) -> None:
        i = 0
        self.erase_taken_space(pkg=pkg)
        while (pkg.location[2] - i > 0 and np.all(
                self.taken_space[pkg.location[0]: pkg.location[0] + pkg.length,
                pkg.location[1]: pkg.location[1] + pkg.width,
                pkg.location[2] - i - 1: pkg.location[2] - i] == Used.NO.value)):
            i += 1
        i -= 0
        if i > 0:
            print(f"check z axis {i}")

        t = list(pkg.location)
        t[2] -= i
        pkg.location = tuple(t)
        self.update_taken_space(pkg=pkg)  # new space occupied

    def check_base(self, pkg: Package, pos: Location) -> bool:
        return True if np.all(np.count_nonzero(self.taken_space[pos[0]: pos[0] + pkg.length + 1,
                                               pos[1]: pos[1] + pkg.width + 1,
                                               pos[2]: pos[2] + 1] == Used.YES.value)) else False

    # def convert_to_json(self):
    #     data_construct = {"ShipmentNumber": self.shipment_number, "packages": []}
    #     data_improve = {"ShipmentNumber": self.shipment_number, "packages": []}
    #
    #     data_construct["packages"] = copy.deepcopy(list_dict_packages(pkgs=self.pkgs_construct))
    #     data_improve["packages"] = copy.deepcopy(list_dict_packages(pkgs=self.pkgs_improve))
    #     print(len(data_improve))
    #     json_construct = json.dumps(data_construct)
    #     json_improve = json.dumps(data_improve)
    #     print(f"{len(json_construct)}, {json_construct}")
    #     print(f"{len(json_improve)}, {json_improve}")
    #
    #     with open("output_construct.json", "w") as json_const:
    #         json_const.write(json_construct)
    #
    #     with open("output_improve.json", "w") as json_improv:
    #         json_improv.write(json_improve)

    def convert_to_json(self):
        data_construct = {"Id": self.shipment_number, "Container": {"Height": str(self.height),
                                                                    "Width": str(self.width),
                                                                    "Depth": str(self.length),
                                                                    "Cost": str(self.cost)},
                          "FirstPackages": [], "SecondPackages": [], "UserId": str(self.userid)}

        data_construct["FirstPackages"] = copy.deepcopy(self.list_dict_packages(pkgs=self.pkgs_construct))
        data_construct["SecondPackages"] = copy.deepcopy(self.list_dict_packages(pkgs=self.pkgs_improve))
        # print(len(data_improve))
        json_construct = json.dumps(data_construct)

        # json_improve = json.dumps(data_improve)
        # print(f"{len(json_construct)}, {json_construct}")
        # print(f"{len(json_improve)}, {json_improve}")
        # with open("output_construct.json", "w") as json_const:
        #     json_const.write(json_construct)
        return io.StringIO(json_construct)
