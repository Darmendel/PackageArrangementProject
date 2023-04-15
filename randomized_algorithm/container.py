# TODO change the weight limit to something reasonable.
import numpy as np
from storage import EfficientStorage

import numpy as np


# TODO: add total possible amount of items.
class Container:

    def __init__(self, height: int, width: int, length: int, weight_limit: int = 5000):
        self.height = height
        self.width = width
        self.length = length
        self.weight_limit = weight_limit
        self.volume = self.height * self.width * self.length
        # self.used_x = EfficientStorage
        # self.used_y =
        # self.used_z =
    # update weight & occupied space.
    def update_container(self):
        pass

    def convert_to_json(self):
        pass
