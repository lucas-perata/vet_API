"use client";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { DataTable } from "./data.table";
import { columns } from "./columns";

export default function ExpensesPerMonthCard() {
  return (
    <Card className="w-[50%]">
      <CardHeader>
        <CardTitle>Gastos del mes </CardTitle>
        <CardDescription></CardDescription>
      </CardHeader>
      <CardContent>
        <DataTable columns={columns} />
      </CardContent>
      <CardFooter className="flex justify-between"></CardFooter>
    </Card>
  );
}
