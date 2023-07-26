import random
from sort import SortedFactory as SF
from package import Package


class ItemSorting:

    def __init__(self, pkgs: list[Package]):
        self.items = pkgs
        self.sort_way = SF()
        self.sort_option = None
        self.sorting()

    '''
    Sorting the boxes according to certain properties.
    '''
    def sorting(self):
        option = random.choice(["volume", "height", "area", "customer_volume", "customer_height", "customer_area"])
        self.items = self.sort_way.sort_algo[option].sort(self.items, option)
        self.sort_option = option

