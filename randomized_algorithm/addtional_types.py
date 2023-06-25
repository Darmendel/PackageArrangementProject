import copy

from constants import *
from package import *
import numpy as np
from numpy import ndarray
from container import Container
from operator import sub


class SmallCuboid:
    def __init__(self, location: Location = None, size: Size = None,
                 box_i: Package = None, box_j: Package = None, all_pkgs: list[Package] = None,
                 index_i: int = -1, index_j: int = -1):
        self.location = location
        self.size = size
        self.box_i = box_i
        self.box_j = box_j
        self.all_pkgs = all_pkgs
        self.volume = 0
        self.end_points = None
        self.interchange_list = []
        self.index_i = self.box_i.index
        self.index_j = self.box_j.index

    #  min x, max x, min y, max y, min z, max z.

    def smallest_cuboid(self, pkg_i: Package, pkg_j: Package):
        self.location = min(pkg_i.location[0], pkg_j.location[0]), min(pkg_i.location[1],
                                                                       pkg_j.location[1]), min(pkg_i.location[2],
                                                                                               pkg_j.location[2])
        for_i = tuple(map(lambda x, y: x + y, pkg_i.location, pkg_i.size))
        for_j = tuple(map(lambda x, y: x + y, pkg_j.location, pkg_j.size))
        self.end_points = max(for_i[0], for_j[0]), max(for_i[1], for_j[1]), max(for_i[2], for_j[2])
        # self.size = max(pkg_i.size[0], pkg_j.size[0]), max(pkg_i.size[1], pkg_j.size[1]), max(pkg_i.size[2],
        #                                                                                       pkg_j.size[2])
        self.size = tuple([abs(i) for i in list(map(sub, self.location, self.end_points))])
        if 0 in self.size:
            print("add types small cuboid")
        self.volume = self.size[0] * self.size[1] * self.size[2]
        # self.end_points = tuple(map(lambda x, y: x + y, self.location, self.size))

    def x_axis(self, cur_pacakge: Package) -> tuple[int, int]:
        if cur_pacakge.location[0] + cur_pacakge.length > self.location[0] or cur_pacakge.location[0] \
                < self.end_points[0]:
            return max(cur_pacakge.location[0], self.location[0]), \
                   min(cur_pacakge.location[0] + self.size[0], self.end_points[0])
        else:
            return -1, -1

    def y_axis(self, cur_pacakge: Package) -> tuple[int, int]:
        if cur_pacakge.location[1] + cur_pacakge.width > self.location[1] or cur_pacakge.location[1] \
                < self.end_points[1]:
            return max(cur_pacakge.location[1], self.location[1]), \
                   min(cur_pacakge.location[1] + self.size[1], self.end_points[1])
        else:
            return -1, -1

    def z_axis(self, cur_pacakge: Package) -> tuple[int, int]:
        if cur_pacakge.location[2] + cur_pacakge.height > self.location[2] or cur_pacakge.location[2] \
                < self.end_points[2]:
            return max(cur_pacakge.location[2], self.location[2]), \
                   min(cur_pacakge.location[2] + self.size[2], self.end_points[2])
        else:
            return -1, -1

    def overlapping_percentage(self, cur_pacakge: Package) -> float:
        funcs = {1: self.x_axis, 2: self.y_axis, 3: self.z_axis}
        dim_overlap = []
        for i in funcs.keys():
            start, end = funcs[i](cur_pacakge)
            if start != -1 and end != -1:
                dim_overlap.append(abs(start - end))
        if len(dim_overlap) == 3:
            return (dim_overlap[0] * dim_overlap[1] * dim_overlap[2]) / self.volume
        else:
            return 0

    def cuboid_overlaps_empty(self, matrix_conflict: ndarray, contdim: Container) -> float:
        penalty_sum = 0
        occupied_percentage = 0
        for col, pkg in enumerate(self.all_pkgs):
            if contdim.volume <= 2 * self.volume:
                if self.overlapping_percentage(pkg) != 0:
                    self.interchange_list.append(copy.deepcopy(pkg))


            else:
                # erase
                if (self.index_i == pkg.index or self.index_j == pkg.index) and self.overlapping_percentage(pkg) == 0:
                    print("a")
                    if self.overlapping_percentage(pkg) == 0:
                        print("check add")

                if self.overlapping_percentage(pkg) != 0:
                    penalty_sum += np.sum(matrix_conflict, axis=0)[col] * self.overlapping_percentage(pkg)
                    occupied_percentage += self.overlapping_percentage(pkg)
                    self.interchange_list.append(copy.deepcopy(pkg))

        return penalty_sum, 100 - occupied_percentage
