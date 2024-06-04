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
import DialogPetExpenseForm from "../pets/details/DialogPetExpenseForm";

export default function MonthExpensesDetail() {
  return (
    <Card className="w-[50%]">
      <CardHeader className="flex flex-row ">
        <CardTitle>
          Gastos del mes <DialogPetExpenseForm create={true} />
        </CardTitle>
        <CardDescription></CardDescription>
      </CardHeader>
      <CardContent>
        <DataTable columns={columns} />
      </CardContent>
      <CardFooter className="flex justify-between"></CardFooter>
    </Card>
  );
}
