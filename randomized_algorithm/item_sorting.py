import random
from sort import SortedFactory as SF
from package import Package


class ItemSorting:

    def __init__(self, pkgs: list[Package]):
        self.items = pkgs
        self.sort_way = SF()
        self.sort_option = None
        self.sorting()

    # Taxability, Priority, Customer_rank
    def sorting(self):
        option = random.choice(["volume", "height", "area", "customer_volume", "customer_height", "customer_area"])
        #  notice we want to sort only the list(first is the label).
        self.items = self.sort_way.sort_algo[option].sort(self.items, option)
        self.sort_option = option
        # print(f"We are sorting by the following option: {option}, found at ItemSorting")
