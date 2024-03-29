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
from addtional_types import *


class ImprovedAlg:
    def __init__(self, cont: Container, pkgs: list[Package]):
        self.cont = cont
        self.pkgs = pkgs
        self.conf_matrix = None
        self.dense_matrix = None
        self.space = None
        self.cuboid_list = []
        self.real_index = np.empty((len(self.pkgs), len(self.pkgs)), dtype=object)

    '''visibility'''

    def value_fv(self, qj: float, vj: int, zj: int):
        (a, b), c = Penaltylevel.MEDIUM.value, 1 / self.cont.height
        return (1 + c * zj) * (a * qj + b * vj)

    '''above'''

    def value_fa(self, qj: float, vj: int, zj: int):
        (a, b), c = Penaltylevel.MEDIUM.value, 1 / self.cont.height
        return (1 + c * zj) * (a * qj + b * vj)

    '''reachability'''

    def value_fr(self, qj: float, vj: float, zj: int, reachability: int):
        (a, b), c = Penaltylevel.MEDIUM.value, 1 / self.cont.height
        return (1 + c * reachability) * (a * qj + b * vj)

    def general(self, pkg_i: Package, pkg_j: Package):  # complete false, false, false
        v, a, r = ImprovedAlg.conflict(pkg_i=pkg_i,
                                       pkg_j=pkg_j)
        if v:
            f_v = self.value_fv(qj=pkg_j.weight,
                                vj=pkg_j.volume,
                                zj=pkg_j.location[2])
        else:
            f_v = 0
        if a:
            f_a = self.value_fa(qj=pkg_j.weight,
                                vj=pkg_j.volume,
                                zj=pkg_j.location[2])
        else:
            f_a = 0
        if r:
            reach = (pkg_j.location[0] + pkg_j.length) - (pkg_i.location[0] + pkg_i.length)
            f_r = self.value_fr(qj=pkg_j.weight,
                                vj=pkg_j.volume,
                                zj=pkg_j.location[2],
                                reachability=reach)
        else:
            f_r = 0

        return f_v + f_a + f_r

    @staticmethod
    def overlap(pkg_i: Package, pkg_j=Package, attr=str):
        location_long_i = {"x": (pkg_i.location[0], pkg_i.length), "y": (pkg_i.location[1], pkg_i.width),
                           "z": (pkg_i.location[2], pkg_i.height)}
        location_long_j = {"x": (pkg_j.location[0], pkg_j.length), "y": (pkg_j.location[1], pkg_j.width),
                           "z": (pkg_j.location[2], pkg_j.height)}

        i_loc, i_len = location_long_i[attr]
        j_loc, j_len = location_long_j[attr]
        return i_loc < j_loc < i_loc + i_len or j_loc < i_loc < j_loc + j_len

    @staticmethod
    def conflict(pkg_i: Package, pkg_j: Package):
        if pkg_i.customer > pkg_j.customer:
            return False, False, False
        else:
            above, visibility, reach = False, False, False
            if pkg_j.location[2] > pkg_i.location[2] + pkg_i.height and ImprovedAlg.overlap(pkg_i, pkg_j, "x") and \
                    ImprovedAlg.overlap(pkg_i, pkg_j, "y"):
                above = True
            if pkg_j.location[0] > pkg_i.location[0] + pkg_i.length and ImprovedAlg.overlap(pkg_i, pkg_j, "y") and \
                    ImprovedAlg.overlap(pkg_i, pkg_j, "z"):
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
                self.real_index[i, j] = item_i.index, item_j.index
                if i != j:
                    if any(ImprovedAlg.conflict(item_i, item_j)):
                        if self.general(pkg_i=item_i, pkg_j=item_j) > 0:
                            c_matrix[i, j] = self.general(pkg_i=item_i, pkg_j=item_j)

        self.conf_matrix = c_matrix

        return c_matrix

    ''' check if a point between the start and the end of all the axis(x, y, z)
     find the list of cuboids that overlap either fully if all x, y,z are inside, or
     partially, at least 2 dimension need to be the same.'''

    def density_empty_matrices(self) -> (ndarray, ndarray):
        matrix = np.asarray(self.pkgs)
        d_matrix = np.zeros(shape=(matrix.shape[0], matrix.shape[0]))
        e_matrix = np.zeros(shape=(matrix.shape[0], matrix.shape[0]))

        for i in range(len(self.pkgs)):

            for j in range(i + 1, len(self.pkgs)):
                cuboid = SmallCuboid(box_i=self.pkgs[i], box_j=self.pkgs[j], all_pkgs=self.pkgs,
                                     index_i=i, index_j=j)
                self.cuboid_list.append(cuboid)
                cuboid.smallest_cuboid(pkg_i=self.pkgs[i], pkg_j=self.pkgs[j])
                d_matrix[i, j], e_matrix[i, j] = cuboid.cuboid_overlaps_empty(matrix_conflict=self.conf_matrix,
                                                                              contdim=self.cont)

        return d_matrix, e_matrix
