"use client";

import { Expense } from "@/types";
import { ColumnDef } from "@tanstack/react-table";
import { Button } from "@/components/ui/button";
import { FaCopy } from "react-icons/fa";
import { ArrowUpDown, MoreHorizontal } from "lucide-react";
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
    id: "actions",
    cell: ({ row }) => {
      const expense = row.original;
      return (
        <Button
          onClick={() =>
            navigator.clipboard.writeText(
              expense.amount + " " + expense.category,
            )
          }
        >
          <FaCopy />
        </Button>
      );
    },
  },
];
