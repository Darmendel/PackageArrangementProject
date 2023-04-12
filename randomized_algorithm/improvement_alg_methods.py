import copy
import random
from itertools import permutations
from input import Input
from package import Package
from container import Container
from auxiliary_methods import *

import copy
import numpy as np
from constants import *
from package import Package
from container import Container


# fv (q j , vj , z j ) + fa (q j , vj , z j ) + fr (q j , vj , z j , δi j )

# TODO: change the value 600 in the 3 methods below to the container's height.


class ImprovedAlg:
    def __init__(self, cont: Container, pkgs: list[Package]):
        self.cont = cont
        self.pkgs = pkgs

    # visibility
    def value_fv(self, qj: float, vj: int, zj: int):
        (a, b), c = Penaltylevel.MEDIUM.value, self.cont.height
        return (1 + c * zj) * (a * qj + b * vj)

    # above
    def value_fa(self, qj: float, vj: int, zj: int):
        (a, b), c = Penaltylevel.MEDIUM.value, self.cont.height
        return (1 + c * zj) * (a * qj + b * vj)

    # reachability
    def value_fr(self, qj: float, vj: float, zj: int, reachability: int):
        (a, b), c = Penaltylevel.MEDIUM.value, self.cont.height
        return (1 + c * reachability) * (a * qj + b * vj)

    def general(self, pkg_i: Package, pkg_j: Package):  # complete false, false, false
        v, a, r = self.conflict(pkg_i=pkg_i, pkg_j=pkg_j)
        if v:
            f_v = self.value_fv(qj=pkg_j.weight, vj=pkg_j.volume, zj=pkg_j.location[2])
        else:
            f_v = 0
        if a:
            f_a = self.value_fa(qj=pkg_j.weight, vj=pkg_j.volume, zj=pkg_j.location[2])
        else:
            f_a = 0
        if r:
            reach = (pkg_j.location[0] + pkg_j.length) - (pkg_i.location[0] + pkg_i.length)
            f_r = self.value_fr(qj=pkg_j.weight, vj=pkg_j.volume, zj=pkg_j.location[2], reachability=reach)
        else:
            f_r = 0
        return f_v + f_a + f_r

    def overlap(self, pkg_i: Package, pkg_j=Package, attr=str):
        location_long_i = {"x": (pkg_i.location[0], pkg_i.length), "y": (pkg_i.location[1], pkg_i.width),
                           "z": (pkg_i.location[2], pkg_i.height)}
        location_long_j = {"x": (pkg_j.location[0], pkg_j.length), "y": (pkg_j.location[1], pkg_j.width),
                           "z": (pkg_j.location[2], pkg_j.height)}

        i_loc, i_len = location_long_i[attr]
        j_loc, j_len = location_long_j[attr]
        return i_loc < j_loc < i_loc + i_len or j_loc < i_loc < j_loc + j_len

    # no conflict  - False, conflict = True:
    def conflict(self, pkg_i: Package, pkg_j: Package):
        if pkg_i.customer > pkg_j.customer:
            return False, False, False
        else:
            above, visibility, reach = False, False, False
            if pkg_j.location[2] > pkg_i.location[2] + pkg_i.height and self.overlap(pkg_i, pkg_j, "x") and \
                    self.overlap(pkg_i, pkg_j, "y"):
                above = True
            if pkg_j.location[0] > pkg_i.location[0] + pkg_i.length and self.overlap(pkg_i, pkg_j, "y") and \
                    self.overlap(pkg_i, pkg_j, "z"):
                visibility = True
            reach = (pkg_j.location[0] + pkg_j.length) - (pkg_i.location[0] + pkg_i.length)
            if reach > min(ARM.HEIGHT.value - pkg_i.location[2], ARM.LENGTH.value):
                reach = True
            return above, visibility, reach

    def conflict_matrix(self) -> object:
        matrix = np.asarray(self.pkgs)
        c_matrix = np.zeros(shape=(matrix.shape[0], matrix.shape[0]))

        for i, item_i in enumerate(matrix):
            for j, item_j in enumerate(matrix):
                if i != j:
                    if self.conflict(item_i, item_j) != (False, False, False):
                        c_matrix[i, j] = self.general(pkg_i=item_i, pkg_j=item_j)
                    else:
                        c_matrix[i, j] = 0

        # print(c_matrix)
        return c_matrix

    def smallest_cuboid(self, pkg_i: Package, pkg_j: Package):
        pass

    def density_matrix(self):
        matrix = np.asarray(self.pkgs)
        d_matric = np.zeros(shape=(matrix.shape[0], matrix.shape[0]))
