from itertools import permutations
import random
from py3dbp import Packer, Bin, Item


class Algorithm:

    def __init__(self, perturbed_list):  # self.container self.final_list bottom_left
        self.container_ = (1600, 800, 600)
        self.full_items = perturbed_list.perturbed_items
        # box format: 'Customer ,Height', 'Width', 'Length', 'Value', 'Volume', 'Priority', 'Above', 'Position',
        # 'taxa', cur_pos
        print("---------------")
        # for i in self.full_items:
        #     print(i)
        self.final_list = []
        self.list_boxes_input()

    def list_boxes_input(self):
        for i in self.full_items[1:]:
            val = i[1], i[2], i[3]
            temp_dict = {val: None for val in i[10]}
            temp_dict["Height"], temp_dict["Width"], temp_dict["Length"] = i[1], i[2], i[3]
            self.final_list.append((int(temp_dict["Length"]), int(temp_dict["Width"]), int(temp_dict["Height"])))
            first = i[10]
            second = i[1], i[2], i[3]
            third = self.final_list
        print(self.final_list)
        print(len(self.final_list))

    def intersects(self, x1, y1, z1, w1, h1, d1, x2, y2, z2, w2, h2, d2):
        return (x1 + w1 > x2 and x2 + w2 > x1
                and y1 + h1 > y2 and y2 + h2 > y1
                and z1 + d1 > z2 and z2 + d2 > z1)

    def feasible(self, pos, box_a, list_pos, list_box, cont_dims):
        # x1, y1, z1, size_x, size_y, size_z = pos  # format: point, below_point (because we try a new PP)
        x1, y1, z1 = pos
        dx1, dy1, dz1 = box_a
        max_x1, max_y1, max_z1 = x1 + dx1, y1 + dy1, z1 + dz1
        if max_x1 > cont_dims[0] or max_y1 > cont_dims[1] or max_z1 > cont_dims[2]:  # exceed container size.
            return False
        for p, s in zip(list_pos, list_box):
            x2, y2, z2 = p
            dx2, dy2, dz2 = s
            max_x2, max_y2, max_z2 = x2 + dx2, y2 + dy2, z2 + dz2
            if max_x1 > x2 and x1 < max_x2 \
                    and max_y1 > y2 and y1 < max_y2 \
                    and max_z1 > z2 and z1 < max_z2:
                return False
        return True

    def bestPoint(self, valid_point, valid_positions):  # valid points format: x, y, z, below.
        max_utilization, cantilever, bad_points = 0, "no", 0
        best_point = -1, -1, -1, 0, 0, 0  # format: point, below_point
        best_position = 0, 0, 0
        for point, valid_pos in zip(valid_point, valid_positions):
            x, y, z, size_x, size_y, size_z = point
            v_x, v_y, v_z = valid_pos
            below_x_y = 0
            if z == 0:
                below_x_y = (self.container_[0] - v_x) * (
                        self.container_[1] - v_y)  # TODO: make sure you are fine with this
            if z > 0:
                below_x_y = x * y

            cur_x_y = v_x * v_y
            if cur_x_y < below_x_y and (max_utilization <= cur_x_y / below_x_y):
                if max_utilization == cur_x_y / below_x_y:
                    if best_point[5] > size_z:
                        best_point = point
                        best_position = valid_pos
                        cantilever = 'no'

                else:
                    max_utilization = cur_x_y / below_x_y
                    best_point = point
                    best_position = valid_pos
                    cantilever = 'no'

            else:
                bad_points += 1

            # elif cur_x_y > below_x_y and below_x_y / cur_x_y > CANTILEVER_PORTION \
            #         and size_x / v_x > CANTILEVER_PORTION and size_y / v_y > CANTILEVER_PORTION \
            #             (max_utilization <= below_x_y / cur_x_y):
            #     if max_utilization == below_x_y / cur_x_y:
            #         if best_point[5] > size_z:
            #             best_point = point
            #             best_position = valid_pos
            #             cantilever = 'yes'
            #     else:
            #         max_utilization = below_x_y / cur_x_y
            #         best_point = point
            #         best_position = valid_pos
            #         cantilever = 'yes'
            # TODO:
            # elif - to doo!!!!! the case in which  cur_x_y is more than the catiliver for all objects, can check with \
            # TODO: len

        if bad_points == len(valid_point):
            cantilever = "mid"
        print(bad_points)
        print(len(valid_point))
        return best_point, best_position, cantilever

    # def bottom_left(self, container_dims, box_dims):
    #     # Step 1: Initialize empty_spaces list with a single space representing the entire container
    #     empty_spaces = [(0, 0, 0, 0, 0, 0)]
    #     box_positions = []  # format: (size_x, size_y, size_z), (cor_x, cor_y, cor_z)
    #     list_boxes = []
    #
    #     retry_list = []
    #
    #     # Step 2: For each box
    #     for box in box_dims:
    #         found_space = False
    #         valid_points = []  # potential points verified.
    #         valid_pos = []  # the shape associated with that point.
    #
    #         for shape in permutations(box):  # TODO: should be in case a current position wasn't introduced.
    #
    #             for i, space in enumerate(empty_spaces):
    #
    #                 if self.feasible(space, shape, box_positions, list_boxes, container_dims):
    #                     valid_points.append(space)
    #                     valid_pos.append(shape)
    #
    #         if len(valid_points) == 0:  # there is no space for the box, doesn't matter shape.
    #             retry_list.append(box)
    #             continue
    #
    #         else:
    #             # TODO: remember the case in which we have to big of an xy face compared with all other.
    #             print("the valid point are: ", valid_points)
    #             print("the emptyspaces are: ", empty_spaces)
    #             best_point, best_shape, cantilever_ = self.bestPoint(valid_points, valid_pos)
    #             if cantilever_ == "mid":
    #                 retry_list.append(box)
    #                 continue
    #
    #             print(empty_spaces)
    #             print(best_point)
    #             box_positions.append((best_point[0], best_point[1], best_point[2]))
    #             list_boxes.append(best_shape)
    #             empty_spaces.remove(best_point)  # removing the best point from the potential points.
    #
    #             if cantilever_ == "no":
    #                 empty_spaces.append((best_point[0], best_point[1], best_point[2] + best_shape[2], *best_shape))
    #                 empty_spaces.append((best_point[0], best_point[1] + best_shape[1], best_point[2], *best_shape))
    #                 empty_spaces.append((best_point[0] + best_shape[0], best_point[1], best_point[2], *best_shape))
    #                 best_point = None
    #
    #     #
    #     for box in retry_list:
    #         last_shape = random.choice(list(permutations(box)))
    #         valid_points = []
    #         valid_pos = []
    #         for space in empty_spaces:
    #             if self.feasible(space, last_shape, box_positions, list_boxes, container_dims):
    #                 valid_points.append(space)
    #                 valid_pos.append(shape)
    #
    #         if len(valid_points) == 0:  # there is no space for the box, doesn't matter shape.
    #             return None, None  # wasn't able to pack.
    #
    #         else:
    #             best_point, best_shape, cantilever_ = self.bestPoint(valid_points, valid_pos)
    #             if cantilever_ == "mid":
    #                 return None, None  # wasn't able to pack.
    #             box_positions.append((best_point[0], best_point[1], best_point[2]))
    #             list_boxes.append(best_shape)
    #             empty_spaces.remove(best_point)  # removing the best point from the potential points.
    #             if cantilever_ == "no":
    #                 empty_spaces.append((best_point[0], best_point[1], best_point[2] + best_shape[2], *best_shape))
    #                 empty_spaces.append((best_point[0], best_point[1] + best_shape[1], best_point[2], *best_shape))
    #                 empty_spaces.append((best_point[0] + best_shape[0], best_point[1], best_point[2], *best_shape))
    #
    #     return box_positions, list_boxes

    def bottom_left(self, container_dims, box_dims):

        empty_spaces = [(0, 0, 0, *container_dims)]
        box_positions = []
        list_boxes = []

        sec_list = []
        # Step 2: For each box
        for box in box_dims:
            found_space = False

            for i, space in enumerate(empty_spaces):
                space_x, space_y, space_z, space_w, space_h, space_d = space

                if set([i for i in permutations(box)]) & set(list_boxes):
                    l = list(set([i for i in permutations(box)]) & set(list_boxes))
                    box = random.choice(l)

                if box[0] <= space_w and box[1] <= space_h and box[2] <= space_d:
                    if not self.feasible((space_x, space_y, space_z), box, box_positions, list_boxes, container_dims) \
                            or (space_x + box[0] > container_dims[0] or space_y + box[1] > container_dims[1] \
                                or space_z + box[2] > container_dims[2]):
                        continue
                    else:
                        box_positions.append((space_x, space_y, space_z))
                        list_boxes.append(box)
                        # Split the space into three smaller spaces based on the dimensions of the box
                        empty_spaces[i:i + 1] = [

                            (space_x + box[0], space_y, space_z, space_w - box[0], space_h, space_d),
                            (space_x, space_y, space_z + box[2], space_w, space_h, space_d - box[2]),
                            (space_x, space_y + box[1], space_z, space_w, space_h - box[1], space_d)]
                        found_space = True
                        break

            if not found_space:
                # print(f"{len(box_positions)}, utilize: {len(box_positions) / len(box_dims)}")
                # return box_positions, list_boxes, len(box_positions) / len(box_dims)
                sec_list.append(box)

        for box in sec_list:
            found_space = False

            for shape in [i for i in permutations(box)]:
                for i, space in enumerate(empty_spaces):
                    space_x, space_y, space_z, space_w, space_h, space_d = space

                    if shape[0] <= space_w and shape[1] <= space_h and shape[2] <= space_d:
                        if not self.feasible((space_x, space_y, space_z), shape, box_positions, list_boxes,
                                             container_dims) \
                                or (space_x + shape[0] > container_dims[0] or space_y + shape[1] > container_dims[1] \
                                    or space_z + shape[2] > container_dims[2]):
                            continue
                        else:
                            box_positions.append((space_x, space_y, space_z))
                            list_boxes.append(shape)
                            # Split the space into three smaller spaces based on the dimensions of the box
                            empty_spaces[i:i + 1] = [
                                (space_x, space_y, space_z + shape[2], space_w, space_h, space_d - shape[2]),
                                (space_x + shape[0], space_y, space_z, space_w - shape[0], space_h, space_d),
                                (space_x, space_y + shape[1], space_z, space_w, space_h - shape[1], space_d)]
                            found_space = True
                            break
                if found_space:
                    break

            if not found_space:
                print(f"{len(box_positions)}, utilize: {len(box_positions) / len(box_dims)}")
                return box_positions, list_boxes, len(box_positions) / len(box_dims), sec_list

        # Step 3: Return box_positions
        return box_positions, list_boxes, 1
