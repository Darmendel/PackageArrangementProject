import random
from package import Package
from item_sorting import ItemSorting

# pertube_main  new
class ItemPerturbation:
    def __init__(self, sorted_items: ItemSorting):
        option_dict = {2: self.func_1_b, 3: self.randomize_list}
        self.perturbed_items = sorted_items.items
        self.sort_by = sorted_items.sort_option
        for key in option_dict.keys():
            option_dict[key]()

    def func_1_b(self):
        temp = set()
        popular_pos = []
        closed = set()

        for i in range(len(self.perturbed_items)):
            k = self.perturbed_items[i]
            if i in closed:
                continue
            for j in range(i + 1, len(self.perturbed_items) - 1):  # i + 2 because we have initiated 1:

                l = self.perturbed_items[j]
                if k.height == l.height and k.width == l.width and k.length == l.length:
                    temp.add(i)
                    temp.add(j)
                    closed.add(i)
                    closed.add(j)
                    joint_pos = list(set(k.positions) & set(l.positions))
                    popular_pos.extend(joint_pos)
                    popular_pos = list(set(popular_pos))

            # if temp and not popular_pos:
            #     print("perturbation problem")
            #     print(temp)
            #     print(popular_pos)

            if temp:
                pos = random.choice(popular_pos)  # all items of the same dimensions will have the same position.
                for idx in temp:
                    self.perturbed_items[idx].cur_pos = pos
                temp = set()

            else:  # stand alone.
                self.perturbed_items[i].cur_pos = random.choice(k.positions)

        # print(closed)
        # for i in closed:
        #     print(f"items that gain same dims {self.perturbed_items[i].cur_pos}")

    def ratio_swap(self, start: int, end: int, idx: list[str]):

        similar_traits = 0
        for i in idx:
            if self.perturbed_items[start].__getattribute__(i) == self.perturbed_items[end].__getattribute__(i):
                similar_traits += 1
        # the following theta calculation which considered for the added randomization preformed better than what is
        # suggested in the article.
        theta = (similar_traits / len(idx))
        if theta == 1 and random.randint(1, 2) == 1:
            self.perturbed_items[end], self.perturbed_items[start] = self.perturbed_items[start], \
                                                                     self.perturbed_items[end]

    def parameter_list(self, sort_by: str) -> list[str]:
        if "_" in sort_by:
            a, b = sort_by.split("_")
            return [a, b]
        return [sort_by]

    def randomize_list(self):

        for i in range(len(self.perturbed_items) - 2, 1, -1):
            self.ratio_swap(i - 1, i, self.parameter_list(self.sort_by))

        self.ratio_swap(len(self.perturbed_items) - 1, 1, self.parameter_list(self.sort_by))
