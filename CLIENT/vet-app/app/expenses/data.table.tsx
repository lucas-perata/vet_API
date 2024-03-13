"use client";
import * as React from "react";
import {
  ColumnDef,
  SortingState,
  flexRender,
  getCoreRowModel,
  getSortedRowModel,
  useReactTable,
} from "@tanstack/react-table";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Button } from "@/components/ui/button";
import { useFetchMonthlyExpenses } from "../hooks/useExpenses";
import useStore from "@/store/store";
import { createInstance } from "@/utils/axiosConfig";
import { useState } from "react";
import { MdNavigateBefore, MdNavigateNext } from "react-icons/md";

interface DataTableProps<TData, TValue> {
  columns: ColumnDef<TData, TValue>[];
  data?: TData[];
  pag?: any;
}

export function DataTable<TData, TValue>({
  columns,
  data,
}: DataTableProps<TData, TValue>) {
  const token = useStore((state) => state.token);
  const axiosI: AxiosInstance = createInstance(token());
  const [pageNumber, setPageNumber] = useState(1);

  const { monthlyExpenses, monthlyExpensesLoading } = useFetchMonthlyExpenses(
    axiosI,
    pageNumber,
  );

  if (data == null) {
    data = monthlyExpensesLoading ? " " : monthlyExpenses?.data.data;
  }

  console.log(data);

  const [sorting, setSorting] = React.useState<SortingState>([]);

  const table = useReactTable({
    data,
    columns,
    getCoreRowModel: getCoreRowModel(),
    manualPagination: true,
    onSortingChange: setSorting,
    getSortedRowModel: getSortedRowModel(),
    state: { sorting },
  });
  if (monthlyExpensesLoading) return <div>Loading...</div>;

  return (
    <div>
      <div className="rounded-md border">
        <Table>
          <TableHeader>
            {table.getHeaderGroups().map((headerGroup) => (
              <TableRow key={headerGroup.id}>
                {headerGroup.headers.map((header) => {
                  return (
                    <TableHead key={header.id}>
                      {header.isPlaceholder
                        ? null
                        : flexRender(
                            header.column.columnDef.header,
                            header.getContext(),
                          )}
                    </TableHead>
                  );
                })}
              </TableRow>
            ))}
          </TableHeader>
          <TableBody>
            {table.getRowModel().rows?.length ? (
              table.getRowModel().rows.map((row) => (
                <TableRow
                  key={row.id}
                  data-state={row.getIsSelected() && "selected"}
                >
                  {row.getVisibleCells().map((cell) => (
                    <TableCell key={cell.id}>
                      {flexRender(
                        cell.column.columnDef.cell,
                        cell.getContext(),
                      )}
                    </TableCell>
                  ))}
                </TableRow>
              ))
            ) : (
              <TableRow>
                <TableCell
                  colSpan={columns.length}
                  className="h-24 text-center"
                >
                  No results.
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </div>
      <div className="flex items-center justify-end space-x-2 py-4">
        <Button
          variant="outline"
          size="sm"
          onClick={() => setPageNumber((page) => page - 1)}
        >
          <MdNavigateBefore />
        </Button>
        <div>{pageNumber}</div>
        <Button
          variant="outline"
          size="sm"
          onClick={() => setPageNumber((page) => page + 1)}
        >
          <MdNavigateNext />
        </Button>
      </div>
    </div>
  );
}
