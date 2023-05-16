import copy
import random
from itertools import permutations
from input import Input
from package import Package
from container import Container
from Preprocess import Preprocess as Pre
from item_sorting import ItemSorting as Its
from Itemperturbation import ItemPerturbation as Itp
from auxiliary_methods import *
from legacy_code import Algorithm as Alg
from lucky_sol import *
import copy
from improvement_alg_methods import *
import numpy as np


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
                found = True
            else:
                break

        if found:
            break

    return constructed_sol


def construction_phase(pkgs: list[Package], cont: Container) -> list[Package]:
    volume = 0
    siz = 0
    sec_size = 0
    best_sol = None
    cur_num, count_v = 0, 0
    for i in range(300):  # 3000
        sorted_items = Its(pkgs)
        items_perturbed = Itp(sorted_items).perturbed_items
        for j, i in enumerate(items_perturbed):
            temp_dict = {val: None for val in i.cur_pos}
            temp_dict["L"], temp_dict["W"], temp_dict["H"] = i.length, i.width, i.height
            i.length = temp_dict[i.cur_pos[0]]
            i.width = temp_dict[i.cur_pos[1]]
            i.height = temp_dict[i.cur_pos[2]]
            i.size = i.length, i.width, i.height

        constructed_solution = construction(pkgs=items_perturbed, cont=cont)
        # constructed_solution, ratio, add_list = bottom_left(container_dims=cont, box_dims=items_perturbed)
        v = 0
        s = 0

        for t in constructed_solution:
            v += t.volume
            s += 1
        if sec_size < s:
            sec_size = s
        if sec_size >= 43:
            print("check")
        if volume <= v:
            volume = v
            best_sol = copy.deepcopy(constructed_solution)
        if v == 764000000:
            count_v += 1
            siz = len(constructed_solution)
        if v == 768000000:
            print("check")
        for pkg in items_perturbed:
            pkg.location = None

    print(f"Max volume: {volume}, {siz}, {sec_size}, amount of max: {count_v}")
    with open("output.txt", "w") as f:
        for box in best_sol:
            a, b, c = box.location
            f.write(f"{box.length} {box.width} {box.height} {a} {b} {c}\n")

    return best_sol


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


def step_1(pkgs=list[Package], cont_info=Container):
    improved_alg = ImprovedAlg(pkgs=pkgs, cont=cont_info)
    conf_mat = improved_alg.conflict_matrix()
    dens_mat, emp_mat = improved_alg.density_empty_matrices()
    return improved_alg, conf_mat, dens_mat, emp_mat


def M_1(conflict: ndarray):
    j = np.argmax(np.sum(conflict, axis=0))
    i = random.choice(
        list(np.where(conflict[:, j][conflict[:, j] > 0] == random.choice((conflict[:, j][conflict[:, j] > 0])))))
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
    first_approach = M_1(conflict=conf)
    second_approach = M_2(density=dens)
    third_approach = M_3(density=dens, empty=emp)
    return first_approach, second_approach, third_approach


def step_3(pkgs=list[Package], improve_alg=ImprovedAlg, box_to_remove=tuple[int, int]):
    cuboid_: SmallCuboid = None
    boxes_to_remove = []
    for cuboid in improve_alg.cuboid_list:
        if (cuboid.index_i == box_to_remove[0] and cuboid.index_j == box_to_remove[1]) \
                or (cuboid.index_i == box_to_remove[1] and cuboid.index_j == box_to_remove[0]) and not boxes_to_remove:
            boxes_to_remove = cuboid.interchange_list
            cuboid_ = cuboid
            break
    x = [item.index for item in boxes_to_remove]
    # print(f"before: {len(pkgs)}, the amount to remove: {len(boxes_to_remove)}")
    vol = 0
    for i in boxes_to_remove:
        vol += i.volume
    if cuboid_.volume * 2 > vol:
        print("check")
    return [i for i in pkgs if i.index not in [item.index for item in boxes_to_remove]]


def step_4(items_left=list[Package], alg=ImprovedAlg, original_list=list[Package]) -> list[Package]:
    conflict_mat = alg.conflict_matrix()
    list_new = [pkg for pkg in original_list if pkg.index not in [i.index for i in items_left]]  # items to check

    print(check)


# erase
# def b(box_to_remove, pkgs=list[Package]) -> tuple[int, int]:
#     matrix = np.asarray(pkgs)
#     for i, item_i in enumerate(matrix):
#         for j, item_j in enumerate(matrix):
#             if i == box_to_remove[0] and j == box_to_remove[1]:
#                 return(item_i.index, item_j.index)
#     return -1, -1
# erase
def check(alg=ImprovedAlg):
    counter = 0
    max = 0
    min = 100
    avg = 0
    for cuboid in alg.cuboid_list:
        if not cuboid.interchange_list:
            counter += 1
        if (len(cuboid.interchange_list)) > max:
            max = (len(cuboid.interchange_list))

        if (len(cuboid.interchange_list)) < min:
            min = (len(cuboid.interchange_list))
        avg += (len(cuboid.interchange_list))
    avg /= len(alg.cuboid_list)
    print("should be:", len(alg.cuboid_list))
    print("problems", counter)


def improvement_alg(pkgs=list[Package], cont_info=Container, real_pkgs=list[Package]) -> list[Package]:
    improve_alg, c, d, e = step_1(pkgs=pkgs, cont_info=cont_info)
    # erase
    check(improve_alg)
    chosen_box = step_2(c, d, e)
    # erase
    # box_to_remove_ = chosen_box[0]
    # call b function.
    real_index = improve_alg.real_index[chosen_box[0]]
    pkgs_removed = step_3(pkgs=pkgs, improve_alg=improve_alg, box_to_remove=real_index)
    pkgs_added = step_4(items_left=pkgs_removed, alg=improve_alg, original_list=real_pkgs)

    print("check")


def start():
    # create boxes
    raw_data = Input("dataset.csv")
    pkgs = create_boxs_from_input(raw_data)
    cont = Container(height=int(raw_data.contdim[0]),
                     width=int(raw_data.contdim[1]),
                     length=int(raw_data.contdim[2]),
                     pkgs_num=len(pkgs))
    copy_list = copy.deepcopy(pkgs)
    # construction algorithm
    solution = construction_phase(pkgs=pkgs, cont=cont)
    # improvement algorithm:
    improved_solution = improvement_alg(pkgs=solution, cont_info=cont, real_pkgs=copy_list)


start()
