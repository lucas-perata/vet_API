"use client";
import useFetchData from "@/app/hooks/useFetch";
import { Card, CardFooter, CardHeader } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { useToast } from "@/components/ui/use-toast";
import { Expense } from "@/types";
import React from "react";
import { deleteExpenseFromPet } from "@/app/actions/petActionsCS";

import DialogExpense from "./DialogExpense";

type Props = {
  id: number;
};

function ExpensesPet({ id }: Props) {
  const { toast } = useToast();

  const config = {
    petUrl: `http://localhost:5193/api/Spending/pet-expenses/${id}`,
  };

  const { data, isLoading, refreshData } = useFetchData(id, config);

  const add = async (petId: number) => {
    await deleteExpenseFromPet(petId);
    toast({
      title: "Gasto agregado",
    });
    refreshData();
  };

  const remove = async (expenseId: number) => {
    await deleteExpenseFromPet(expenseId);
    toast({
      title: "Gasto eliminado",
    });
    refreshData();
  };

  if (isLoading) return <p>Loading...</p>;
  if (!data) return <p>No data</p>;

  return (
    <div>
      <h2 className="scroll-m-20 pb-2 text-3xl font-semibold tracking-tight first:mt-0 self-center flex items-center gap-4">
        Gastos asociados
        <DialogExpense petId={id} />
      </h2>
      <div className="overflow-scroll max-h-[57vh]">
        <div className="flex flex-col gap-4">
          {data.map((expense: Expense) => (
            <Card key={expense.id} className="w-[400px] border-green-500">
              <CardHeader>
                <div>{expense.category}</div>
                <div>{expense.description}</div>
                <div>{expense.amount}</div>
                <div>{expense.date}</div>
              </CardHeader>

              <CardFooter>
                <div onClick={() => remove(expense.id)}>
                  <Button>borrar</Button>
                </div>
              </CardFooter>
            </Card>
          ))}
        </div>
      </div>
    </div>
  );
}

export default ExpensesPet;
