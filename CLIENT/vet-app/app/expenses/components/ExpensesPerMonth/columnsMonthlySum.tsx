"use client";
import { Expense } from "@/types";
import { ColumnDef } from "@tanstack/react-table";

export const columnsMonthlySum: ColumnDef<Expense>[] = [
  {
    accessorKey: "month",
    header: "Mes",
    cell: ({ row }) => {
      const amount = parseFloat(row.getValue("month"));
      let formatted = "";

      switch (amount) {
        case 1:
          formatted = "Enero";
          break;
        case 2:
          formatted = "Febrero";
          break;
        case 3:
          formatted = "Marzo";
          break;
        case 4:
          formatted = "Abril";
          break;
        case 5:
          formatted = "Mayo";
          break;
        case 6:
          formatted = "Junio";
          break;
        case 7:
          formatted = "Julio";
          break;
        case 8:
          formatted = "Agosto";
          break;
        case 9:
          formatted = "Septiembre";
          break;
        case 10:
          formatted = "Octubre";
          break;
        case 11:
          formatted = "Noviembre";
          break;
        case 12:
          formatted = "Diciembre";
          break;
        default:
          break;
      }
      return <div>{formatted}</div>;
    },
  },
  {
    accessorKey: "totalSpending",
    header: "Total",
    cell: ({ row }) => {
      const amount = parseFloat(row.getValue("totalSpending"));
      const formatted = new Intl.NumberFormat("es-AR", {
        style: "currency",
        currency: "ARS",
      }).format(amount);
      return <div className="text-right font-medium">{formatted}</div>;
    },
  },
];
