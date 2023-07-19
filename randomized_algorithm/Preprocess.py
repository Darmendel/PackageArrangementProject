import random

PREPROCESS_ITERATIONS = 3


# send the input in constructor.

class Preprocess:
    def __init__(self, box_input):
        self.cur_boxes = list(box_input.boxes)  # boxes before the preprocess stage.
        self.dict_options = {1: self.option_a, 2: self.option_b, 3: self.option_c}
        self.organize()

    def organize(self):  # memoization. will be implemented using a dict.
        for i in range(PREPROCESS_ITERATIONS):
            option = random.randint(1, 1)  # should be 1 to 3
            print(f"option: {option}")
            self.dict_options[option]()

    def option_a(self):
        pass

    def box_values(self, changed, i, j):

        if changed == "Length":
            return self.cur_boxes[j][1], self.cur_boxes[j][2], self.cur_boxes[j][3] + self.cur_boxes[i][3]
        elif changed == "Width":
            return self.cur_boxes[j][1], self.cur_boxes[j][2] + self.cur_boxes[i][2], self.cur_boxes[j][3]
        elif changed == "Height":
            return self.cur_boxes[j][1] + self.cur_boxes[i][1], self.cur_boxes[j][2], self.cur_boxes[j][3]

    def combined_item(self, changed, i, j):
        t_box = self.box_values(changed, i, j)
        # print(f"i is: {i}, j is {j}")
        return [max(self.cur_boxes[i][0], self.cur_boxes[j][0]), t_box[0], t_box[1], t_box[2],
                self.cur_boxes[j][4] + self.cur_boxes[i][4],
                self.cur_boxes[j][5] + self.cur_boxes[i][5], self.cur_boxes[j][6] + self.cur_boxes[i][6],
                max(self.cur_boxes[j][7], self.cur_boxes[i][7]),
                list(set(self.cur_boxes[j][8]) & set(self.cur_boxes[j][8]))]

    def items_left(self, index_used, temp_list):
        for i, j in enumerate(self.cur_boxes[1:], 1):
            if i not in index_used:
                temp_list.append(j)
        self.cur_boxes = temp_list

    '''2 of the 3 (width,length, height) are the same.'''

    # box format: 'Customer ,Height', 'Width', 'Length', 'Value', 'Volume', 'Priority', 'Above', 'Position'
    def option_b(self):
        temp_list = [self.cur_boxes[0]]
        index_used = []
        # idx start with one because of the slicing.
        for idx, i in enumerate(self.cur_boxes[1:len(self.cur_boxes) - 1], 1):
            for idy, j in enumerate(self.cur_boxes[idx + 1:], idx + 1):
                if idx in index_used:
                    break
                if idy in index_used:
                    continue
                # what to do if the 3 of them are equal? which one to choose? on random?
                if self.cur_boxes[idx][1] == self.cur_boxes[idy][1] and self.cur_boxes[idx][2] == self.cur_boxes[idy][
                    2]:
                    temp_list.append(self.combined_item("Length", idx, idy))
                    index_used.extend([idx, idy])
                    break  # cannot combine the same i'th box twice, in case all 3 equal.

                elif self.cur_boxes[idx][2] == self.cur_boxes[idy][2] and self.cur_boxes[idx][3] == self.cur_boxes[idy][
                    3]:
                    temp_list.append(self.combined_item("Height", idx, idy))
                    index_used.extend([idx, idy])
                    break
                elif self.cur_boxes[idx][1] == self.cur_boxes[idy][1] and self.cur_boxes[idx][3] == self.cur_boxes[idy][
                    3]:
                    temp_list.append(self.combined_item("Width", idx, idy))
                    index_used.extend([idx, idy])
                    break
        # cur_boxes will contain the combined boxes

        # deals with items which weren't combined
        self.items_left(index_used, temp_list)

    ''' 3 out of 3 are the same. '''

    def option_c(self):
        temp_list, index_used, option_dict = [self.cur_boxes[0]], [], {1: "Length", 2: "Width", 3: "Height"}
        # idx start with one because of the slicing.
        for idx, i in enumerate(self.cur_boxes[1:len(self.cur_boxes) - 1], 1):
            for idy, j in enumerate(self.cur_boxes[idx + 1:], idx + 1):
                if idx in index_used:
                    break
                elif idy in index_used:
                    continue
                # maybe should wait untill all processed and then choose the most suitable one.
                elif self.cur_boxes[idx][1] == self.cur_boxes[idy][1] and self.cur_boxes[idx][2] == self.cur_boxes[idy][
                    2] and self.cur_boxes[idx][3] == self.cur_boxes[idy][3]:
                    option = random.randint(1, 3)
                    temp_list.append(self.combined_item(option_dict[option], idx, idy))
                    index_used.extend([idx, idy])

        self.items_left(index_used, temp_list)
