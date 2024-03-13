import * as React from "react";
import { Button } from "@/components/ui/button";
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
import { useFetchSum } from "../hooks/useExpenses";

export default function TotalExpensesCard() {
  const token = useStore((state) => state.token);
  const axiosI: AxiosInstance = createInstance(token());

  const { sumExpenses, sumExpensesLoading, sumExpensesError } =
    useFetchSum(axiosI);

  return (
    <Card className="w-[350px]">
      <CardHeader>
        <CardTitle>Gastos anuales</CardTitle>
        <CardDescription></CardDescription>
      </CardHeader>
      <CardContent>${sumExpenses}</CardContent>
      <CardFooter className="flex justify-between"></CardFooter>
    </Card>
  );
}
