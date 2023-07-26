from data import testdata
import unittest
import main
from input import InputJson
from package import Package
from container import Container
import copy


class TestAlg(unittest.TestCase):
    def test_parsing(self):
        raw_data = InputJson(testdata.d)
        self.assertEqual(len(raw_data.pkgs), 43)
        self.assertEqual(raw_data.contdim[0], 600)
        self.assertEqual(raw_data.contdim[1], 800)
        self.assertEqual(raw_data.contdim[2], 1600)
        self.assertEqual(int(raw_data.shipment_number), 100)
        self.assertEqual(raw_data.cost, 700)

    def test_solutions(self):
        raw_data = InputJson(testdata.d)
        pkgs = raw_data.pkgs
        cont = Container(height=int(raw_data.contdim[0]),
                         width=int(raw_data.contdim[1]),
                         length=int(raw_data.contdim[2]),
                         shipment_number=raw_data.shipment_number,
                         pkgs_num=len(pkgs),
                         userid=raw_data.userId,
                         cost=raw_data.cost)
        copy_list = copy.deepcopy(pkgs)
        # construction algorithm
        # solution = construction_phase(pkgs=pkgs, cont=cont)
        s1 = main.TimeLimit(run_time_alg=.1,
                            alg_function=main.construction_phase,
                            pkgs=pkgs,
                            container_dim=cont)
        self.assertLess(len(s1.run_algorithm()), 44)


if __name__ == '__main__':
    unittest.main()
