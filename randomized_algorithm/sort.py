import random
from package import Package


# fix priority to be an average.

class BaseSort:

    def sort(self, items: list[Package], comparator: str) -> list[Package]:
        raise NotImplementedError()


# # box format: 'Customer ,Height', 'Width', 'Length', 'Value', 'Volume', 'Priority', 'Above', 'Position', 'taxa
# priority, customer code - stability matters


# TODO fix to decreasing order and not increasing order.
class MergeSort(BaseSort):

    @staticmethod
    def merge(fis: list[Package], sec: list[Package], comparator: str):
        i, j = 0, 0
        fis1, sec2 = comparator.split("_")
        merged = []
        while i < len(fis) and j < len(sec):

            # second if checks taxability in case the value of the comparator is equal.
            if fis[i].__getattribute__(fis1) > sec[j].__getattribute__(fis1) or \
                    (fis[i].__getattribute__(fis1) == sec[j].__getattribute__(fis1) and
                     fis[i].__getattribute__(sec2) > sec[j].__getattribute__(sec2)):
                merged.append(fis[i])
                i += 1
            elif fis[i].__getattribute__(fis1) < sec[j].__getattribute__(fis1) or\
                    (fis[i].__getattribute__(fis1) == sec[j].__getattribute__(fis1) and
                     fis[i].__getattribute__(sec2) < sec[j].__getattribute__(sec2)):
                merged.append(sec[j])
                j += 1
            # ties are broken randomly.
            elif random.randint(1, 2) < 2:
                merged.append(fis[i])
                i += 1
            else:
                merged.append(sec[j])
                j += 1

        while i < len(fis):
            merged.append(fis[i])
            i += 1
        while j < len(sec):
            merged.append(sec[j])
            j += 1

        return merged

    def sort(self, items: list[Package], comparator: str) -> list[Package]:
        if len(items) == 1 or len(items) == 2:
            if len(items) == 1:
                return items
            fis1, sec2 = comparator.split("_")
            if items[0].__getattribute__(fis1) > items[1].__getattribute__(fis1) or (
                    items[0].__getattribute__(fis1) == items[1].__getattribute__(fis1) and
                    items[0].__getattribute__(sec2) > items[1].__getattribute__(sec2)):
                return items
            elif items[0].__getattribute__(fis1) < items[1].__getattribute__(fis1) or (
                    items[0].__getattribute__(fis1) == items[1].__getattribute__(fis1) and
                    items[0].__getattribute__(sec2) < items[1].__getattribute__(sec2)):
                return [items[1], items[0]]
            # ties are broken randomly.
            elif random.randint(1, 2) < 2:
                return items
            else:
                return [items[1], items[0]]

        left = self.sort(items[0: len(items) // 2], comparator)
        right = self.sort(items[len(items) // 2:], comparator)
        return MergeSort.merge(left, right, comparator)


# volume, height, area.
class SimpleSort(BaseSort):
    def sort(self, items: list[Package], comparator: str) -> list[Package]:
        copy_items = list(items)
        copy_items.sort(key=lambda x: x.__getattribute__(comparator), reverse=True)
        return copy_items


class SortedFactory:
    def __init__(self):
        self.sort_algo = {"volume": SimpleSort(),
                          "height": SimpleSort(),
                          "area": SimpleSort(),
                          "customer_volume": MergeSort(),
                          "customer_height": MergeSort(),
                          "customer_area": MergeSort()
                          }

