import copy
import math
import random
from itertools import permutations
from input import Input, InputJson
from package import Package
from container import Container
from Preprocess import Preprocess as Pre
from item_sorting import ItemSorting as Its
from Itemperturbation import ItemPerturbation as Itp
from auxiliary_methods import *
from legacy_code import Algorithm as Alg
import copy
from improvement_alg_methods import *
import numpy as np
import alg_send
import os
import threading

CONSTRUCTION_ITERATIONS = 1000000
STOP_CONSTRUCTION, STOP_IMPROVEMENT = False, False
IMPROVEMENTALG_ITERATIONS = 1000000

FINAL_CONSTRUCTION_PACKAGING = []
FINAL_IMPROVEMENT_PACKAGING = []


class TimeLimit:
    def __init__(self, run_time_alg, alg_function, pkgs, container_dim, real_pkgs=None):
        self.timeout_duration = run_time_alg
        self.alg_function = alg_function
        self.pkgs = pkgs
        self.cont_dim = container_dim
        self.real_pkgs = real_pkgs
        self.timer = None

    def timer_ends(self):
        global STOP_CONSTRUCTION, STOP_IMPROVEMENT
        if not STOP_CONSTRUCTION:
            STOP_CONSTRUCTION = True
        else:
            STOP_IMPROVEMENT = True

    def run_algorithm(self):
        self.timer = threading.Timer(self.timeout_duration, self.timer_ends)
        self.timer.start()
        try:
            if not self.real_pkgs:
                result = self.alg_function(self.pkgs, self.cont_dim)
            else:
                result = self.alg_function(self.pkgs, self.cont_dim, self.real_pkgs)
            return result

        except TimeoutError:
            if not self.real_pkgs:
                return FINAL_CONSTRUCTION_PACKAGING
            return FINAL_IMPROVEMENT_PACKAGING

        finally:
            self.timer.cancel()


def construction(pkgs: list[Package], cont: Container) -> (list[Package], Container):
    init_point = [(0, 0, 0)]
    constructed_sol, retry_list = [], []

    for iter_num, pkg in enumerate(pkgs):
        if iter_num == 0 or not constructed_sol:
            # (pos: Location, box_a: Package, list_box: list[Package], cont_dims: Container)

            if check_feasibility((0, 0, 0), pkg, [], cont):
                pkg.location = (0, 0, 0)
                init_point.remove((0, 0, 0))
                init_point.extend([(0 + pkg.length, 0, 0), (0, 0 + pkg.width, 0), (0, 0, 0 + pkg.height)])
                constructed_sol.append(pkg)
                # cont.update_taken_space(pkg=pkg)
            else:
                retry_list.append(pkg)
        else:
            valid_points = []
            for pos_point in init_point:
                if check_feasibility(pos_point, pkg, constructed_sol, cont):
                    valid_points.append(pos_point)
            if valid_points:
                valid_points.sort(key=lambda location: (location[0], location[1], location[2]))
                pkg.location = valid_points[0]
                init_point.remove(valid_points[0])
                init_point = update_3DEPL(items_packed=constructed_sol,
                                          extreme_points=init_point,
                                          new_pkg=pkg,
                                          cont_dims=cont)
                constructed_sol.append(copy.deepcopy(pkg))
                # cont.update_taken_space(pkg=pkg)
            else:
                retry_list.append(pkg)
                # break

    for pkg in retry_list:
        found = False
        for shape in [i for i in permutations((pkg.length, pkg.width, pkg.height))]:
            valid_points = []
            pkg.length, pkg.width, pkg.height = shape  # fix should be length, width, height.
            for pos_point in init_point:
                if check_feasibility(pos_point, pkg, constructed_sol, cont):
                    valid_points.append(pos_point)
            if valid_points:
                valid_points.sort(key=lambda location: (location[0], location[1], location[2]))
                pkg.location = valid_points[0]
                init_point.remove(valid_points[0])
                init_point = update_3DEPL(items_packed=constructed_sol,
                                          extreme_points=init_point,
                                          new_pkg=pkg,
                                          cont_dims=cont)
                constructed_sol.append(copy.deepcopy(pkg))
                # cont.update_taken_space(pkg=pkg)
                found = True
            else:
                break

        if found:
            break

    return constructed_sol


def construction_phase(pkgs: list[Package], cont: Container) -> list[Package]:
    global FINAL_CONSTRUCTION_PACKAGING
    volume = 0
    siz = 0
    sec_size = 0
    best_sol = None
    cur_num, count_v = 0, 0
    for i in range(CONSTRUCTION_ITERATIONS):  # 3000
        if STOP_CONSTRUCTION:
            break

        print(f"The number of iteration {i}")
        sorted_items = Its(pkgs)
        if len(sorted_items.items) > 5:
            items_perturbed = Itp(sorted_items).perturbed_items
            for j, i in enumerate(items_perturbed):
                temp_dict = {val: None for val in i.cur_pos}
                temp_dict["L"], temp_dict["W"], temp_dict["H"] = i.length, i.width, i.height
                i.length = temp_dict[i.cur_pos[0]]
                i.width = temp_dict[i.cur_pos[1]]
                i.height = temp_dict[i.cur_pos[2]]
                i.size = i.length, i.width, i.height
        else:
            items_perturbed = sorted_items.items

        constructed_solution = construction(pkgs=items_perturbed, cont=cont)

        v = 0
        s = 0

        for t in constructed_solution:
            v += t.volume
            s += 1
        if sec_size < s:
            sec_size = s

        if volume <= v:
            volume = v
            best_sol = copy.deepcopy(constructed_solution)
            FINAL_CONSTRUCTION_PACKAGING = copy.deepcopy(best_sol)

        for pkg in items_perturbed:
            pkg.location = None

    print(f"Max volume: {volume}, {siz}, {sec_size}, amount of max: {count_v}")
    with open("output.txt", "w") as f:
        for box in best_sol:
            a, b, c = box.location
            f.write(f"{box.length} {box.width} {box.height} {a} {b} {c}\n")

    return FINAL_CONSTRUCTION_PACKAGING


def create_boxs_from_input(input_: Input):
    pkgs = []
    for index_, i in enumerate(input_.boxes[1:]):
        pkgs.append(Package(customer=i[0],
                            height=i[1],
                            width=i[2],
                            length=i[3],
                            weight=i[4],
                            priority=i[5],
                            above=i[6],
                            positions=i[7],
                            index=index_
                            )
                    )

    return pkgs


def step_1(pkgs: list[Package], cont_info: Container):
    improved_alg = ImprovedAlg(pkgs=pkgs, cont=cont_info)
    conf_mat = improved_alg.conflict_matrix()
    dens_mat, emp_mat = improved_alg.density_empty_matrices()
    return improved_alg, conf_mat, dens_mat, emp_mat


def M_1(conflict: ndarray):
    j = np.argmax(np.sum(conflict, axis=0))
    try:
        i = random.choice(
            list(np.where(conflict[:, j][conflict[:, j] > 0] == random.choice((conflict[:, j][conflict[:, j] > 0])))))
    except IndexError:
        return 0, 0
    return i[random.randint(0, len(i) - 1)], j


def M_2(density: ndarray):
    highest = density.max()
    highest_values = []
    for i in range(len(density)):
        for j in range(i + 1, len(density)):
            if highest == density[i, j]:
                highest_values.append((i, j))
    return random.choice(highest_values)


def M_3(density: ndarray, empty: ndarray):
    combined_mat = np.add(density, empty)
    return M_2(density=combined_mat)


def step_2(conf: ndarray, dens: ndarray, emp: ndarray):
    try:
        first_approach = M_1(conflict=conf)
    except IndexError:
        first_approach = 0
    try:
        second_approach = M_2(density=dens)
    except IndexError:
        second_approach = 0
    try:
        third_approach = M_3(density=dens, empty=emp)
    except IndexError:
        third_approach = 0

    return first_approach, second_approach, third_approach


def step_3(pkgs: list[Package], improve_alg: ImprovedAlg, box_to_remove: tuple[int, int]):
    cuboid_: SmallCuboid = None
    boxes_to_remove = []
    for cuboid in improve_alg.cuboid_list:
        if (cuboid.index_i == box_to_remove[0] and cuboid.index_j == box_to_remove[1]) \
                or (
                cuboid.index_i == box_to_remove[1] and cuboid.index_j == box_to_remove[0]) and not boxes_to_remove:
            boxes_to_remove = cuboid.interchange_list
            cuboid_ = cuboid
            break
    x = [item.index for item in boxes_to_remove]
    # print(f"before: {len(pkgs)}, the amount to remove: {len(boxes_to_remove)}")
    # erase 192 - 197
    vol = 0
    for i in boxes_to_remove:
        vol += i.volume
    try:
        if cuboid_.volume * 2 > vol:
            print("step_3")
    except AttributeError:
        print("The chosen boxes create cuboid the size of at least half of the container")
        return [], []
    return boxes_to_remove, [i for i in pkgs if i.index not in [item.index for item in boxes_to_remove]]


#  length, width, height
def start_pos_4(pkgs: list[Package]) -> list[Location]:
    pkg_x, pkg_min_x = pkgs[0], pkgs[0].location[0]
    pkg_y, pkg_min_y = pkgs[0], pkgs[0].location[1]
    pkg_z, pkg_min_z = pkgs[0], pkgs[0].location[2]

    for pkg in pkgs[1:]:
        if pkg.location[0] < pkg_min_x:
            pkg_min_x = pkg.location[0]
            pkg_x = pkg
        if pkg.location[1] < pkg_min_y:
            pkg_min_y = pkg.location[1]
            pkg_y = pkg
        if pkg.location[2] < pkg_min_z:
            pkg_min_z = pkg.location[2]
            pkg_z = pkg
    return list(dict.fromkeys([pkg_x, pkg_y, pkg_z]))


def step_4_reconstruction(pkgs: list[Package], cont: Container, init_point: list[Location],
                          constructed_sol: list[Package] = []) \
        -> (list[Package], Container):
    retry_list = []

    for iter_num, pkg in enumerate(pkgs):

        valid_points = []
        for pos_point in init_point:
            if check_feasibility(pos_point, pkg, constructed_sol, cont):
                valid_points.append(pos_point)
        if valid_points:
            valid_points.sort(key=lambda location: (location[0], location[1], location[2]))
            pkg.location = valid_points[0]
            init_point.remove(valid_points[0])
            init_point = update_3DEPL(items_packed=constructed_sol,
                                      extreme_points=init_point,
                                      new_pkg=pkg,
                                      cont_dims=cont)
            constructed_sol.append(copy.deepcopy(pkg))
        else:
            retry_list.append(pkg)
            # break

    for pkg in retry_list:
        found = False
        for shape in [i for i in permutations((pkg.length, pkg.width, pkg.height))]:
            valid_points = []
            pkg.length, pkg.width, pkg.height = shape
            for pos_point in init_point:
                if check_feasibility(pos_point, pkg, constructed_sol, cont):
                    valid_points.append(pos_point)
            if valid_points:
                valid_points.sort(key=lambda location: (location[0], location[1], location[2]))
                pkg.location = valid_points[0]
                init_point.remove(valid_points[0])
                init_point = update_3DEPL(items_packed=constructed_sol,
                                          extreme_points=init_point,
                                          new_pkg=pkg,
                                          cont_dims=cont)
                constructed_sol.append(copy.deepcopy(pkg))
                found = True
            else:
                break

        if found:
            break

    return constructed_sol


def step_4(items_left: list[Package], alg: ImprovedAlg, original_list: list[Package], deleted_pkgs: list[Package],
           container_dim: Container) -> list[Package]:
    conflict_mat = alg.conflict_matrix()
    list_new = [pkg for pkg in original_list if pkg.index not in [i.index for i in items_left]]  # items to check
    list_to_put = []
    for i in range(conflict_mat.shape[0]):
        real_index, fake = alg.real_index[i, 0]
        if real_index in [unplaced.index for unplaced in list_new]:
            sumrow = np.sum(conflict_mat, axis=1)[i]
            sumcolumn = np.sum(conflict_mat, axis=0)[i]
            pkg_put = [pkg for pkg in original_list if pkg.index == real_index][0]
            list_to_put.append((- sumrow - sumcolumn, pkg_put))

    list_to_put.sort(key=lambda cost: (cost[0]), reverse=True)
    items_not_put = [pkg for pkg in list_new if pkg.index not in [i[1].index for i in list_to_put]]
    items_not_put = [(0, pkg) for pkg in items_not_put]
    list_to_put.extend(items_not_put)
    potential_point_pkgs = start_pos_4(pkgs=deleted_pkgs)
    potential_points = [pkg.location for pkg in potential_point_pkgs]
    pkgs_reconstruct = [i[1] for i in list_to_put]
    multi_drop_sol = step_4_reconstruction(pkgs=pkgs_reconstruct, cont=container_dim,
                                           init_point=potential_points, constructed_sol=items_left)

    print("check step 4")
    return multi_drop_sol


# def decrease_size_gcd(pkgs_packed: list[Package], container_dims: Container) -> (list[Package], int):
#     gcd = math.gcd(math.gcd(container_dims.length, container_dims.width), container_dims.height)
#     for pkg in pkgs_packed:
#         gcd = math.gcd(math.gcd(math.gcd(pkg.location[0], pkg.location[1]), pkg.location[2]), gcd)
#     container_dims.size = container_dims.length / gcd, container_dims.width / gcd, container_dims.height / gcd
#     for pkg in pkgs_packed:
#         pkg.size = pkg.size[0] / gcd, pkg.size[1] / gcd, pkg.size[2] / gcd
#         pkg.location = pkg.location[0] / gcd, pkg.location[1] / gcd, pkg.location[2] / gcd
#     return container_dims, pkgs_packed, gcd
#
#
# def increase_size_gcd(gcd: int, pkgs_packed: list[Package], container_dims: Container) -> (list[Package],
# int): container_dims.size = container_dims.length[0] * gcd, container_dims.width[1] * gcd, container_dims.height[2]
# * gcd for pkg in pkgs_packed: pkg.size = pkg.size[0] * gcd, pkg.size[1] * gcd, pkg.size[2] * gcd pkg.location =
# pkg.location[0] * gcd, pkg.location[1] * gcd, pkg.location[2] * gcd return pkgs_packed


# full implementation of the algorithm from 2007.
def step_5(pkgs_not_packed: list[Package], pkgs_packed: list[Package], container_dims: Container) -> list[Package]:
    y = len(pkgs_packed)
    for pkg in pkgs_packed:
        container_dims.update_taken_space(pkg=pkg)
    pkgs_packed.sort(key=lambda s_pkg: s_pkg.location[0])  # sort via x.
    for pkg in pkgs_packed:  # change location
        container_dims.move_along_x_axis(pkg=pkg)
    pkgs_packed.sort(key=lambda s_pkg: s_pkg.location[1])  # sort via y.
    for pkg in pkgs_packed:  # change location
        container_dims.move_along_y_axis(pkg=pkg)
    pkgs_packed.sort(key=lambda s_pkg: s_pkg.location[2])  # sort via z.
    for pkg in pkgs_packed:  # change location
        container_dims.move_along_z_axis(pkg=pkg)

    for pkg in pkgs_packed:  # for future iterations.
        container_dims.erase_taken_space(pkg=pkg)

    final_points = []
    pkgs_packed.sort(key=lambda s_pkg: s_pkg.location[0], reverse=True)
    final_points.append(step_5_initialize_points(pkgs=pkgs_packed))
    pkgs_packed.sort(key=lambda s_pkg: s_pkg.location[1], reverse=True)
    final_points.append(step_5_initialize_points(pkgs=pkgs_packed))
    pkgs_packed.sort(key=lambda s_pkg: s_pkg.location[2], reverse=True)
    final_points.append(step_5_initialize_points(pkgs=pkgs_packed))
    z = len(pkgs_packed)
    if y != z:
        pass

    # final_pkgs = step_4_reconstruction(pkgs=pkgs_not_packed, cont=container_dims, init_point=final_points,
    #                                    constructed_sol=pkgs_packed)

    print("step 5")
    # return final_pkgs + pkgs_packed
    return pkgs_packed


def step_5_initialize_points(pkgs: list[Package]) -> Location:
    return (pkgs[0].location[0] + pkgs[0].size[0], pkgs[0].location[1] + pkgs[0].size[1],
            pkgs[0].location[2] + pkgs[0].size[2])


def improvement_alg(pkgs: list[Package], cont_info: Container, real_pkgs: list[Package]) -> list[Package]:
    global FINAL_IMPROVEMENT_PACKAGING
    volume, max_vol = 0, 0
    best_multi_drop_sol, temp_sol = [], []
    for i in range(IMPROVEMENTALG_ITERATIONS):
        if STOP_IMPROVEMENT:
            break
        improve_alg, c, d, e = step_1(pkgs=pkgs, cont_info=cont_info)
        chosen_box = step_2(c, d, e)
        real_index = improve_alg.real_index[chosen_box[0]]
        old_pkgs, pkgs_removed = step_3(pkgs=pkgs, improve_alg=improve_alg, box_to_remove=real_index)
        if old_pkgs and pkgs_removed:
            pkgs_added = step_4(items_left=pkgs_removed,
                                alg=improve_alg,
                                original_list=real_pkgs,
                                deleted_pkgs=old_pkgs,
                                container_dim=cont_info)
            pkgs_not_packed = [pkg for pkg in real_pkgs if pkg.index not in [idx.index for idx in pkgs_added]]
            pkgs_copied = copy.deepcopy(pkgs_added)

            x = copy.deepcopy(pkgs_copied)
            pkgs_added = step_5(pkgs_not_packed=pkgs_not_packed, pkgs_packed=pkgs_copied, container_dims=cont_info)

            for t in pkgs_added:
                volume += t.volume

            if max_vol <= volume:
                max_vol = volume
                best_multi_drop_sol = copy.deepcopy(pkgs_added)
                FINAL_IMPROVEMENT_PACKAGING = copy.deepcopy(pkgs_added)
                temp_sol = copy.deepcopy(x)
            volume = 0
            pkgs_added, pkgs_copied = [], []

    print(f"max volume mdclp: {max_vol}")
    with open("outputmdclp.txt", "w") as f:
        for box in best_multi_drop_sol:
            a, b, c = box.location
            f.write(f"{box.length} {box.width} {box.height} {a} {b} {c}\n")

    # temp erase - created to check
    print(f"max volume mdclp: {max_vol}")
    with open("outputmdclpf.txt", "w") as f:
        for box in temp_sol:
            a, b, c = box.location
            f.write(f"{box.length} {box.width} {box.height} {a} {b} {c}\n")

    return FINAL_IMPROVEMENT_PACKAGING


def start(boxes_json):
    raw_data = InputJson(boxes_json)
    pkgs = raw_data.pkgs

    cont = Container(height=int(raw_data.contdim[0]),
                     width=int(raw_data.contdim[1]),
                     length=int(raw_data.contdim[2]),
                     shipment_number=raw_data.shipment_number,
                     pkgs_num=len(pkgs),
                     userid=raw_data.userId,
                     cost=raw_data.cost)
    copy_list = copy.deepcopy(pkgs)
    # construction algorithm
    # solution = construction_phase(pkgs=pkgs, cont=cont)
    s1 = TimeLimit(run_time_alg=0.01, alg_function=construction_phase, pkgs=pkgs, container_dim=cont)
    solution = s1.run_algorithm()
    cont.pkgs_construct = copy.deepcopy(solution)
    # improvement algorithm:
    # improved_solution = improvement_alg(pkgs=solution, cont_info=cont, real_pkgs=copy_list)
    s2 = TimeLimit(run_time_alg=20,
                   alg_function=improvement_alg,
                   pkgs=solution,
                   container_dim=cont,
                   real_pkgs=copy_list)
    try:
        improved_solution = s2.run_algorithm()
    except:
        improved_solution = []
    cont.pkgs_improve = copy.deepcopy(improved_solution)

    final_json = cont.convert_to_json()
    # only when connecting to server:
    alg_send.send_to_server(json_file=final_json)

# start(boxes_json='input.json')
