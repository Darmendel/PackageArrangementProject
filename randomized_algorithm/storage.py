from typing import TypeVar, Generic
from constants import *
import numpy as np
from numpy import ndarray

T = TypeVar('T')


class EfficientStorage(Generic[T]):

    def __init__(self, size: int) -> None:
        self.used_storage: ndarray[T] = np.zeros(shape=size * 2)
        self.used_flags: ndarray = np.empty(shape=size * 2)
        self.used_flags.fill(Exist.FALSE.value)
        self.size = size
        self.counter = 0

    def push_first(self, f_start_pos: T, f_end_pos: T) -> None:
        self.used_storage[0], self.used_storage[1] = f_start_pos, f_end_pos
        self.used_flags[0], self.used_flags[1] = Point.START.value, Point.END.value
        self.counter += 2

    # return Exist.False.value if value not found.
    def binary_adjusted_search(self, val: T):
        def inner(start_idx: int, end_idx: int):
            if self.used_storage[end_idx] == val:
                return end_idx
            if start_idx == end_idx:
                return Exist.FALSE.value

            mid_idx = (start_idx + end_idx) // 2
            if self.used_storage[mid_idx] < val:
                return inner(mid_idx + 1, end_idx)
            else:
                return inner(start_idx, mid_idx)

        return inner(start_idx=0, end_idx=self.counter - 1)

    # push only after checking that it's possible.
    # TODO: add perfect allocation - 2 numbers are found but the pattern is 0 1(one finished the other begun)
    # TODO: add counter.
    def push_in_order(self, start_pos: int, end_pos: int):
        # self.counter += 1
        if self.binary_adjusted_search(start_pos) == Exist.FALSE.value and \
                self.binary_adjusted_search(end_pos) == Exist.FALSE.value:
            s_pos = self.find_left(start_pos) + 1
            self.used_storage[s_pos] = start_pos
            self.used_flags[s_pos] = Point.START.value
            self.counter += 1
            e_pos = self.find_left(end_pos) + 1
            self.used_storage[e_pos] = end_pos
            self.used_flags[e_pos] = Point.END.value
            self.counter += 1
        # our end point is the start of an existing point.
        # input example: 1 0 (for the used flags)
        # output example 1 0 0
        elif self.binary_adjusted_search(end_pos) == Exist.FALSE.value:
            exist_end_pos = self.binary_adjusted_search(start_pos)
            self.used_flags[exist_end_pos] = Point.START.value
            e_pos = self.find_left(end_pos) + 1
            self.used_storage[e_pos] = end_pos
            self.used_flags[e_pos] = Point.END.value
            self.counter += 1

            # produces:   1 0 --> 1 0 0

        # our start point is the end of existing point
        # input example: 1 0 (for the used flags)
        # output example: 1 1 0
        elif self.binary_adjusted_search(start_pos) == Exist.FALSE.value:
            exist_start_pos = self.binary_adjusted_search(end_pos)
            self.used_flags[exist_start_pos] = Point.END.value
            s_pos = self.find_left(start_pos) + 1
            self.used_storage[s_pos] = start_pos
            self.used_flags[s_pos] = Point.START.value
            self.counter += 1

    def find_left(self, val: T):

        def inner(start_idx: int, end_idx: int):
            if self.used_storage[end_idx] < val and end_idx == self.counter - 1 or \
                    self.used_storage[end_idx] < val < self.used_storage[end_idx + 1]:
                return end_idx

            mid_idx = (start_idx + end_idx) // 2
            if self.used_storage[mid_idx] < val:
                return inner(mid_idx + 1, end_idx)
            else:
                return inner(start_idx, mid_idx)

        return inner(start_idx=0, end_idx=self.counter - 1)

