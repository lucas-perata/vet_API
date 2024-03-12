"use client";
import { useToast } from "@/components/ui/use-toast";
import { AxiosInstance } from "axios";
import { useQuery } from "@tanstack/react-query";

export function useFetchExpenses(instance: AxiosInstance) {
  const { data, isLoading, isError } = useQuery({
    queryKey: ["expenses"],
    queryFn: async () => {
      const { data } = await instance.get("api/spending/all-spendings");
      return data;
    },
  });
  return { data, isLoading, isError };
}

export function useFetchMonthlyExpenses(
  instance: AxiosInstance,
  pageParam?: number,
) {
  if (pageParam == null) {
    pageParam = 1;
  }
  const {
    data: monthlyExpenses,
    isLoading: monthlyExpensesLoading,
    isError: monthlyExpensesError,
  } = useQuery({
    queryKey: ["monthlyExpenses", pageParam], // Include pageParam in the query key
    queryFn: async () => {
      const response = await instance.get(
        "api/spending/monthly?PageSize=10&PageNumber=" + pageParam,
        {
          responseType: "json",
          headers: { "Content-Type": "application/json" },
        },
      );
      return { data: response, headers: response.headers };
    },
  });
  return { monthlyExpenses, monthlyExpensesLoading, monthlyExpensesError };
}

export function useFetchSum(instance: AxiosInstance) {
  const {
    data: sumExpenses,
    isLoading: sumExpensesLoading,
    isError: sumExpensesError,
  } = useQuery({
    queryKey: ["sumYearExpenses"],
    queryFn: async () => {
      const { data } = await instance.get("api/spending/sum-spendings-year");
      return data;
    },
  });
  return { sumExpenses, sumExpensesLoading, sumExpensesError };
}

export function useFetchMonthlySum(
  instance: AxiosInstance,
  year: number,
  month: number,
) {
  const {
    data: monthlySumExpense,
    isLoading: monthlySumExpensesLoading,
    isError: monthlySumExpensesError,
  } = useQuery({
    queryKey: ["sumMonthly", year, month],
    queryFn: async () => {
      const { data } = await instance.get(
        `api/spending/monthly-sum?month=${month}&year=${year}`,
      );
      return data;
    },
  });
  return {
    monthlySumExpense,
    monthlySumExpensesLoading,
    monthlySumExpensesError,
  };
}

export function useFetchPreviousMonthsSum(instance: AxiosInstance) {
  const { data, isLoading, isError } = useQuery({
    queryKey: ["sumPreviousMonths"],
    queryFn: async () => {
      const { data } = await instance.get("api/Spending/sum-previous-months");
      return data;
    },
  });
  return { data, isLoading, isError };
}

export function useFetchCatSumMonthly(instance: AxiosInstance) {
  const { data, isLoading, isError } = useQuery({
    queryKey: ["monthlyExpenses"],
    queryFn: async () => {
      const { data } = await instance.get("api/Spending/category/sum");
      return data;
    },
  });
  return { data, isLoading, isError };
}

export function useMutateMonthlySum(
  instance: AxiosInstance,
  year: number,
  month: number,
) {
  const { mutate: monthlySum } = useMutation({
    mutationFn: async (year: number, month: number) =>
      await instance.get(
        `api/spending/monthly-sum?month=${month}&year=${year}`,
      ),
  });
}
