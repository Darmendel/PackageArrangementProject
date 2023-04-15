from constants import *
from itertools import permutations


# subject to a change
class Package:

    def __init__(self, customer: int, height: int, width: int, length: int, weight: float,
                 priority: int, above: Above, positions: list[Location] = None, location: Location = None):
        self.customer = customer  # represents customer code.
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
