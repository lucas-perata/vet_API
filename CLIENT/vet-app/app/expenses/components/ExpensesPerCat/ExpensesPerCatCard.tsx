import * as React from "react";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { useFetchCatSumMonthly } from "@/app/hooks/useExpenses";
import useStore from "@/store/store";
import { createInstance } from "@/utils/axiosConfig";
import CategorySumChart from "./CategorySumChart";

export default function ExpensesPerCatCard() {
  const token = useStore((state) => state.token);
  const axiosI = createInstance(token());
  const { data, isLoading, isError } = useFetchCatSumMonthly(axiosI);
  if (isLoading) return <div>Loading...</div>;
  console.log(data);
  return (
    <Card className="w-[350px]">
      <CardHeader>
        <CardTitle>Gastos por categoría</CardTitle>
        <CardDescription>gráfico + porcentaje</CardDescription>
      </CardHeader>
      <CardContent>
        {data.map((cat) => (
          <div>
            <p>{cat.categoryName}</p>
            <p>{cat.total}</p>
          </div>
        ))}
        <CategorySumChart data={data} />
      </CardContent>

      <CardFooter className="flex justify-between"></CardFooter>
    </Card>
  );
}
