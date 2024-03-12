"use client";
import React, { useState } from "react";
import { useFetchExpenses, useFetchMonthlySum } from "../hooks/useExpenses";
import useStore from "@/store/store";
import { createInstance } from "@/utils/axiosConfig";
import { AxiosInstance } from "axios";
import MonthlyExpensesCard from "./components/ExpensesPerMonth/MonthlyExpensesCard";
import TotalExpensesCard from "./TotalExpensesCard";
import ExpensesPerCatCard from "./components/ExpensesPerCat/ExpensesPerCatCard";
import ExpensesPerMonthCard from "./ExpensesPerMonthCard";

function page() {
  const token = useStore((state) => state.token);
  const axiosI: AxiosInstance = createInstance(token());

  const now = new Date();
  const year = now.getFullYear();
  const month = now.getMonth() + 1;

  const { data, isLoading, isError } = useFetchExpenses(axiosI);

  const {
    monthlySumExpense,
    monthlySumExpensesLoading,
    monthlySumExpensesError,
  } = useFetchMonthlySum(axiosI, year, month);

  if (isLoading || monthlySumExpensesLoading) return <p>Loading...</p>;

  if (isError && monthlySumExpensesError) return <p>Error...</p>;

  return (
    <div className="flex flex-row flex-grow gap-5 justify-center">
      <div className="flex flex-col gap-5">
        <MonthlyExpensesCard />
        <TotalExpensesCard />
      </div>

      <ExpensesPerMonthCard />
      <ExpensesPerCatCard instance={axiosI} />
    </div>
  );
}

export default page;
