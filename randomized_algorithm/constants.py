from enum import Enum

from enum import unique

Location = tuple[int, int, int]


class Above(Enum):
    NOT_ALLOWED = 0
    ALLOWED = 1


class Penaltylevel(Enum):
    LOW = 2.5 * 10 ** -3, 5.0 * 10 ** -3
    MEDIUM = 7.5 * 10 ** -3, 15.0 * 10 ** -3
    HIGH = 12.5 * 10 ** -3, 25.0 * 10 ** -3
    VERY_HIGH = 17.5 * 10 ** -3, 35.0 * 10 ** -3


@unique
# measurement units - cm.
class ARM(Enum):
    LENGTH = 60
    HEIGHT = 200
