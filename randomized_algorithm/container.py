# TODO change the weight limit to something reasonable.

class Container:

    def __init__(self, height: int, width: int, length: int, weight_limit: int = 5000):
        self.height = height
        self.width = width
        self.length = length
        self.weight_limit = weight_limit
        self.volume = self.height * self.width * self.length




