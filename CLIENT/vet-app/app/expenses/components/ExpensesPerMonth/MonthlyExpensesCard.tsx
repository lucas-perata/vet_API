import * as React from "react";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import useStore from "@/store/store";
import { createInstance } from "@/utils/axiosConfig";
import { useFetchPreviousMonthsSum } from "@/app/hooks/useExpenses";
import { columnsMonthlySum } from "./columnsMonthlySum";
import { DataTableMonthSum } from "./data.table.monthsum";

export default function MonthlyExpensesCard() {
  const token = useStore((state) => state.token);
  const axiosI = createInstance(token());

  const { data, isLoading, isError } = useFetchPreviousMonthsSum(axiosI);

  const today = new Date();
  const currentMonth = today.getMonth() + 1;

  if (isLoading) return <div>Loading...</div>;
  let sorted = data.sort((a, b) => {
    const monthA = a.month;
    const monthB = b.month;

    if (monthA === currentMonth) {
      return -1;
    } else if (monthB === currentMonth) {
      return 1;
    }

    const diffA = (monthA - currentMonth + 12) % 12;
    const diffB = (monthB - currentMonth + 12) % 12;

    return diffB - diffA;
  });

  return (
    <Card className="w-[350px] h-[500px]">
      <CardHeader>
        <CardTitle>Gastos por mes</CardTitle>
        <CardDescription></CardDescription>
      </CardHeader>
      <CardContent className="p-5">
        <DataTableMonthSum columns={columnsMonthlySum} data={sorted} />
      </CardContent>
      <CardFooter className="flex justify-between"></CardFooter>
    </Card>
  );
}
