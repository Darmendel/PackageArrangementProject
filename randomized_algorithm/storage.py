from typing import TypeVar, Generic
from constants import *
import numpy as np
from numpy import ndarray

T = TypeVar('T')


class EfficientStorage(Generic[T]):

    def __init__(self, size: int) -> None:
        self.used_storage: ndarray = np.zeros(shape=size * 2)
        self.used_flags: ndarray = np.zeros(shape=size * 2)
        self.size = size

    def push_first(self, f_start_pos: T, f_end_pos: T):
        self.used_storage[0], self.used_storage[1] = f_start_pos, f_end_pos

    # return -1 if value not found.
    def binary_adjusted_search(self, pos: T):
        def inner(sorted_storage: ndarray[T]):
            if sorted_storage[self.size // 2] == pos:
                return self.size // 2
            else:
                if sorted_storage[self.size // 2] > pos:
                    return inner(sorted_storage[: self.size // 2])
                else:
                    return inner(sorted_storage[self.size // 2 + 1:])

    # works for even.
    def find_left(self, pos: T):
        i = self.size

        def inner(sorted_storage: ndarray[T]):
            if sorted_storage[(i - 1) // 2] < pos < sorted_storage[i // 2]:
                return sorted_storage[(i - 1) // 2]
            elif pos > sorted_storage[i // 2] and (i // 2) + 1 == self.size:
                return sorted_storage[self.size - 1]
            elif pos < sorted_storage[(i - 1) // 2] and (i - 1) // 2 == 0:
                return sorted_storage[(i - 1) // 2 - 1]
            else:
                if pos < sorted_storage(i - 1 // 2):
                    return inner(sorted_storage=sorted_storage[:(i - 1) // 2 - 1])
                elif pos > sorted_storage(i // 2):
                    return inner(sorted_storage=sorted_storage[(i // 2) + 1:])


def push_in_order(self, start_pos: int, end_pos: int):
    if self.binary_adjusted_search(start_pos) == Exist.TRUE.value:
        pass
    if self.binary_adjusted_search(end_pos) == Exist.TRUE.value:
        pass
