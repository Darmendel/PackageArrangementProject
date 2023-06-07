from constants import *
from package import *
import numpy as np
from numpy import ndarray
from container import Container


class SmallCuboid:
    def __init__(self, location: Location = None, size: Size = None,
                 box_i: Package = None, box_j: Package = None, all_pkgs: list[Package] = None):
        self.location = location
        self.size = size
        self.box_i = box_i
        self.box_j = box_j
        self.all_pkgs = all_pkgs
        self.volume = 0
        self.end_points = None

    def smallest_cuboid(self, pkg_i: Package, pkg_j: Package):
        self.location = min(pkg_i[0], pkg_j[0]), min(pkg_i[1], pkg_j[1]), min(pkg_i[2], pkg_j[2])
        self.size = max(pkg_i[0], pkg_j[0]), max(pkg_i[1], pkg_j[1]), max(pkg_i[2], pkg_j[2])
        self.volume = self.location[0] * self.location[1] * self.location[2]
        self.end_points = tuple(map(lambda x, y: x + y, self.locationm, self.size))

    def empty_space(self):
        pass

    def x_axis(self, cur_pacakge: Package) -> tuple[int, int]:
        if cur_pacakge.location[0] > self.end_points[0] or cur_pacakge.location[0] < self.location[0] + self.size[0]:
            return max(cur_pacakge.location[0], self.location[0]),\
                   min(cur_pacakge.location[0] + self.size[0], self.end_points[0])
        else:
            return 0, 0

    def y_axis(self, cur_pacakge: Package):
        if cur_pacakge.location[1] > self.end_points[1] or cur_pacakge.location[1] < self.location[1] + self.size[1]:
            return max(cur_pacakge.location[1], self.location[1]), \
                   min(cur_pacakge.location[1] + self.size[1], self.end_points[1])
        else:
            return 0, 0

    def z_axis(self, cur_pacakge: Package):
        if cur_pacakge.location[2] > self.end_points[2] or cur_pacakge.location[2] < self.location[2] + self.size[2]:
            return max(cur_pacakge.location[2], self.location[2]), \
                   min(cur_pacakge.location[2] + self.size[2], self.end_points[2])
        else:
            return 0, 0

    def overlapping(self, cur_pacakge: Package) -> list:
        funcs = {1: self.x_axis, 2: self.y_axis, 3: self.z_axis}
        res = []
        for i in funcs.keys:
            pass

    def cuboid_overlaps(self, matrix_conflict: ndarray, contdim: Container) -> float:
        for pkg in self.all_pkgs:
            if contdim.volume <= 2 * self.volume:
                pass
            else:
                self.overlapping(pkg)
