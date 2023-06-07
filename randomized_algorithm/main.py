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
                init_point = update_3DEPL(items_packed=constructed_sol, extreme_points=init_point, new_pkg=pkg,
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
                init_point = update_3DEPL(items_packed=constructed_sol, extreme_points=init_point, new_pkg=pkg,
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
    for i in input_.boxes[1:]:
        pkgs.append(Package(customer=i[0], height=i[1], width=i[2], length=i[3], weight=i[4],
                            priority=i[5], above=i[6], positions=i[7]))

    return pkgs


def improvement_alg(pkgs=list[Package], cont_info=Container):
    improved_alg = ImprovedAlg(pkgs=pkgs, cont=cont_info)
    conf_mat = improved_alg.conflict_matrix()
    print(conf_mat)
    print(conf_mat.shape)
    dens_mat = improved_alg.density_matrix()

    pass


def start():
    # create boxes
    raw_data = Input("dataset.csv")
    pkgs = create_boxs_from_input(raw_data)
    cont = Container(height=int(raw_data.contdim[0]),
                     width=int(raw_data.contdim[1]),
                     length=int(raw_data.contdim[2]),
                     pkgs_num=len(pkgs))
    # construction algorithm
    solution = construction_phase(pkgs=pkgs, cont=cont)
    # improvement algorithm:
    improved_solution = improvement_alg(pkgs=solution, cont_info=cont)


start()