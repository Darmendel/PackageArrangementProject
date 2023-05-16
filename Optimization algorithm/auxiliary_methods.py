from constants import *
from package import Package
from container import Container
import numpy as np


def check_feasibility(pos: Location, box_a: Package, list_box: list[Package], cont_dims: Container) -> bool:
    x1, y1, z1 = pos
    dx1, dy1, dz1 = box_a.size
    max_x1, max_y1, max_z1 = x1 + dx1, y1 + dy1, z1 + dz1
    if max_x1 > cont_dims.length or max_y1 > cont_dims.width or max_z1 > cont_dims.height:  # exceed container size.
        return False

    for used_box in list_box:
        x2, y2, z2 = used_box.location
        dx2, dy2, dz2 = used_box.size
        max_x2, max_y2, max_z2 = x2 + dx2, y2 + dy2, z2 + dz2
        if max_x1 > x2 and x1 < max_x2 \
                and max_y1 > y2 and y1 < max_y2 \
                and max_z1 > z2 and z1 < max_z2:
            return False

    return True


def better_point(point_a: Location, point_b: Location) -> Location:
    if (point_a[2] < point_b[2]) or (point_a[2] == point_b[2] and point_a[1] < point_b[1]) or \
            (point_a[2] == point_b[2] and point_a[1] == point_b[1] and point_a[0] < point_b[0]):
        return point_a
    else:
        return point_b


def checkYX(k_pkg: Package, nbox: Package) -> bool:
    n_minx, n_maxx = nbox.location[0], nbox.location[0] + nbox.length
    n_miny, n_maxy = nbox.location[1], nbox.location[1] + nbox.width
    return n_minx <= k_pkg.location[0] <= n_maxx and n_miny <= k_pkg.location[1] <= n_maxy


def checkYZ(k_pkg: Package, nbox: Package) -> bool:
    n_miny, n_maxy = nbox.location[1], nbox.location[1] + nbox.width
    n_minz, n_maxz = nbox.location[2], nbox.location[2] + nbox.height
    return n_miny <= k_pkg.location[1] <= n_maxy and n_minz <= k_pkg.location[2] <= n_maxz


def checkXZ(k_pkg: Package, nbox: Package) -> bool:
    n_minx, n_maxx = nbox.location[0], nbox.location[0] + nbox.length
    n_minz, n_maxz = nbox.location[2], nbox.location[2] + nbox.height
    return n_minx <= k_pkg.location[0] <= n_maxx and n_minz <= k_pkg.location[2] <= n_maxz


def can_take_projection(k_pkg: Package, nbox: Package, side: str) -> bool:
    check_side = {"YX": checkYX, "YZ": checkYZ, "XY": checkYX, "XZ": checkXZ, "ZX": checkXZ, "ZY": checkYZ}
    return check_side[side](k_pkg, nbox)


# TODO: fix small bug about the corners.
# TODO: check to seek if picking the smallest distance produce better results.

# implementation of the algorithm of finding potential points  by  Crainic  et al. (2008)
def update_3DEPL(items_packed: list[Package], extreme_points: list[Location], new_pkg: Package,
                 cont_dims: Container) -> list[Location]:
    # check about can take projection.
    max_bound = [-1 for i in range(6)]  # format:
    a = len(extreme_points)
    new_eps = set()
    # ep_dict = {}
    #
    # if new_pkg.location[2] > 0:
    #     ep_dict["XY"] = (0 + cont_dims.length, new_pkg.location[1] + new_pkg.width, new_pkg.location[2]), \
    #                     new_pkg.location[2]
    # if new_pkg.location[1] > 0:
    #     ep_dict["ZY"] = (new_pkg.location[0], new_pkg.location[1] + new_pkg.width, 0 + cont_dims.height), \
    #                     new_pkg.location[1]
    # if new_pkg.location[0] > 0:
    #     ep_dict["ZX"] = (new_pkg.location[0] + new_pkg.length, new_pkg.location[1], 0 + cont_dims.height), \
    #                     new_pkg.location[0]
    #
    # if new_pkg.location[2] + new_pkg.height + 1 < cont_dims.height:
    #     ep_dict["YX"] = (0 + cont_dims.length, new_pkg.location[1] + new_pkg.width, new_pkg.location[2]), \
    #                     (cont_dims.height - (new_pkg.location[2] + new_pkg.height))
    #
    # if new_pkg.location[0] + new_pkg.length + 1 < cont_dims.length:
    #     ep_dict["YZ"] = (new_pkg.location[0], new_pkg.location[1] + new_pkg.width, 0 + cont_dims.height), \
    #                     (cont_dims.length - (new_pkg.location[0] + new_pkg.length))
    #
    # if new_pkg.location[1] + new_pkg.width + 1 < cont_dims.width:
    #     ep_dict["XZ"] = (new_pkg.location[0] + new_pkg.length, new_pkg.location[1], 0 + cont_dims.height), \
    #                     (cont_dims.width - (new_pkg.location[1] + new_pkg.width))

    for item in items_packed:
        if can_take_projection(new_pkg, item, "YX") and item.location[0] + item.length > max_bound[0]:
            new_eps.add((item.location[0] + item.length, new_pkg.location[1] + new_pkg.width, new_pkg.location[2]))
            n_p = item.location[0] + item.length, new_pkg.location[1] + new_pkg.width, new_pkg.location[2]

            max_bound[0] = item.location[0] + item.length

        if can_take_projection(new_pkg, item, "YZ") and item.location[2] + item.height > max_bound[2]:
            new_eps.add((new_pkg.location[0], new_pkg.location[1] + new_pkg.width, item.location[2] + item.height))
            max_bound[2] = item.location[2] + item.height
            n_p = new_pkg.location[0], new_pkg.location[1] + new_pkg.width, item.location[2] + item.height

        if can_take_projection(new_pkg, item, "XY") and item.location[1] + item.width > max_bound[1]:
            new_eps.add((new_pkg.location[0] + new_pkg.length, item.location[1] + item.width, new_pkg.location[2]))
            max_bound[1] = item.location[1] + item.width
            n_p = new_pkg.location[0] + new_pkg.length, item.location[1] + item.width, new_pkg.location[2]

            # max_bound[0] = item.location[0] + item.length

        if can_take_projection(new_pkg, item, "XZ") and item.location[2] + item.height > max_bound[2]:
            new_eps.add((new_pkg.location[0] + new_pkg.length, new_pkg.location[1], item.location[2] + item.height))
            max_bound[3] = item.location[2] + item.height > max_bound[2]
            n_p = new_pkg.location[0] + new_pkg.length, new_pkg.location[1], item.location[2] + item.height

        if can_take_projection(new_pkg, item, "ZX") and item.location[0] + item.length > max_bound[4]:
            new_eps.add((item.location[0] + item.length, new_pkg.location[1], new_pkg.location[2] + new_pkg.height))
            max_bound[0] = item.location[0] + item.length
            n_p = item.location[0] + item.length, new_pkg.location[1], new_pkg.location[2] + new_pkg.height

            # max_bound[0] = item.location[0] + item.length

        if can_take_projection(new_pkg, item, "ZY") and item.location[1] + item.width > max_bound[5]:
            new_eps.add((new_pkg.location[0], item.location[1] + item.width, new_pkg.location[2] + new_pkg.height))
            max_bound[5] = item.location[1] + item.width
            n_p = new_pkg.location[0], item.location[1] + item.width, new_pkg.location[2] + new_pkg.height

            # max_bound[0] = item.location[0] + item.length

    # d = list(set(extreme_points).union(set([i[0] for i in ep_dict.values()])))
    new_eps.add((new_pkg.location[0] + new_pkg.length, 0, 0))  # yz,zy
    new_eps.add((0, new_pkg.location[1] + new_pkg.width, 0))  # zx. xz
    new_eps.add((0, 0, new_pkg.location[2] + new_pkg.height))  # xy yx
    d = list(set(extreme_points).union(new_eps))
    d = list(dict.fromkeys(d))
    # print(len(d) - a)
    return d
