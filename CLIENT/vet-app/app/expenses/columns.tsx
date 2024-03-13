"use client";
import { Expense } from "@/types";
import { ColumnDef } from "@tanstack/react-table";
import { Button } from "@/components/ui/button";
import { ArrowUpDown } from "lucide-react";
import DeleteExpenseButton from "./components/MonthlyExpenses/DeleteExpenseButton";
import DialogPetExpenseForm from "../pets/details/DialogPetExpenseForm";

export const columns: ColumnDef<Expense>[] = [
  {
    accessorKey: "description",
    header: "Descripción",
  },
  {
    accessorKey: "category",
    header: "Categoría",
  },
  {
    accessorKey: "amount",
    header: ({ column }) => {
      return (
        <Button
          variant="ghost"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
        >
          Total
          <ArrowUpDown className="ml-2 h-4 w-4" />
        </Button>
      );
    },
    cell: ({ row }) => {
      const amount = parseFloat(row.getValue("amount"));
      const formatted = new Intl.NumberFormat("es-AR", {
        style: "currency",
        currency: "ARS",
      }).format(amount);
      return <div className="text-right font-medium">{formatted}</div>;
    },
  },
  {
    accessorKey: "date",
    header: "Fecha",
  },
  {
    id: "delete",
    cell: ({ row }) => {
      const expense = row.original;
      return <DeleteExpenseButton id={expense.id} />;
    },
  },
  {
    id: "edit",
    cell: ({ row }) => {
      const expense = row.original;
      return (
        <DialogPetExpenseForm
          petId={expense.petId}
          existingData={expense}
          expenseId={expense.id}
          create={false}
        />
      );
    },
  },
];
