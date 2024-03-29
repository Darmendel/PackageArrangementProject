﻿using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public class DeliveryServiceHelper : IDeliveryServiceHelper
    {
        private static int CalculateCost(List<Package> packages = null, IContainer container = null)
        {
            if (packages == null && container == null) return -1;
            int cost = 0;

            if (container != null)
            {
                try
                {
                    int c = Int32.Parse(container.Cost);
                    cost += c;
                }
                catch (FormatException) { return -1; }
            }

            return cost;
        }

        public static int Cost(Delivery delivery)
        {
            if (delivery == null) return -1;
            return CalculateCost(delivery.FirstPackages, delivery.Container);
        }

        public static int Cost(List<Package> packages = null, IContainer? container = null)
        {
            return CalculateCost(packages, container);
        }

        int IDeliveryServiceHelper.Cost(Delivery delivery) => DeliveryServiceHelper.Cost(delivery);

        int IDeliveryServiceHelper.Cost(List<Package> packages = null, IContainer? container = null) =>
            DeliveryServiceHelper.Cost(packages, container);

    }
}
