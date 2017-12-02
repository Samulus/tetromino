#! /usr/bin/env python

import sys

from functools import reduce


class ANSIColor:
    DEFAULT = "\033[0m"
    GREEN = "\033[32m"
    RED = "\033[31m"
    MAGENTA = "\033[35m"
    YELLOW = "\033[33m"


def git_status_to_color(status):
    return {
        " M": ANSIColor.RED,
        "M ": ANSIColor.GREEN,
        "??": ANSIColor.RED,
    }[status]

def git_status_to_letter(status):
    return {
        " M": "M",
        "M ": "A",
        "??": "U",
    }[status]


def main():
    lines = [i for i in sys.stdin.read().splitlines() if not i.endswith("/")]

    def lst_idx(la, lb):
        for i, j in enumerate(la):
            if i >= len(lb) or j != lb[i]:
                return i

        return 0

    last_path = []
    for line in lines:
        status = line[:2]
        path = line[3:].split("/")

        idx = lst_idx(last_path, path)
        for i in range(idx, len(path)):
            letter = git_status_to_letter(status)
            indent = reduce(lambda acc, x: acc + len(x), path[:i], 0)
            padding = "{:<%d}" % (indent + len(letter) + 1)
            print("%s%s%s%s %s" % (padding.format(""),
                                   git_status_to_color(status),
                                   letter,
                                   ANSIColor.DEFAULT,
                                   path[i]))

        last_path = path

    return 0


if __name__ == "__main__":
    sys.exit(main())
