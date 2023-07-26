from constants import *
from itertools import permutations


'''
defines a Package(box)
customer - represents customer code.
'''
class Package:

    def __init__(self, customer: int, height: int, width: int, length: int, weight: float = 0,
                 unique_idx: int = 0,
                 priority: int = -1, above: Above = Above.ALLOWED, positions: list[Location] = None,
                 location: Location = None,
                 index: int = -1):
        self.customer = customer
        self.height = height
        self.width = width
        self.length = length
        self.weight = weight
        self.volume = self.height * self.width * self.length
        self.priority = priority
        self.above = above
        self.positions = [i for i in permutations(("L", "W", "H"))]
        self.taxability = max(self.volume, self.weight)
        self.customer_area = self.width * self.height
        self.area = self.length * self.width
        self.cur_pos = None
        self.location = location
        self.size = self.length, self.width, self.height
        self.index = index
        self.unique_idx = unique_idx
