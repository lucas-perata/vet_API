"use client";
import { Card, CardFooter, CardHeader } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { useToast } from "@/components/ui/use-toast";
import { Expense } from "@/types";
import React from "react";
import { useQuery } from "@tanstack/react-query";
import { createInstance } from "@/utils/axiosConfig";
import useStore from "@/store/store";
import { FaTrash } from "react-icons/fa";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import DialogUpdateExpense from "./DialogUpdateExpense";

type Props = {
  id: number;
};

function ExpensesPet({ id }: Props) {
  const { toast } = useToast();
  const token = useStore((state) => state.token);

  const axiosI = createInstance(token());

  const { data, isLoading, isError } = useQuery({
    queryKey: ["petExpenses"],
    queryFn: async () => {
      const { data } = await axiosI.get(
        `api/spending/pet-expenses/${id}?PageSize=10`,
      );
      return data;
    },
  });

  const queryClient = useQueryClient();

  const { mutate: deleteExpense, isLoading: deleteLoading } = useMutation({
    mutationFn: async (id: number) => await axiosI.delete(`api/spending/${id}`),
    onSuccess: () => {
      toast({ description: "Gasto eliminado" });
      queryClient.invalidateQueries(["petExpenses"]);
    },
    onError: () => {
      toast({ description: "Error" });
    },
  });

  if (isLoading) return <div>Loading...</div>;
  if (isError) return <div>There was an error...</div>;

  return (
    <div>
      <h2 className="scroll-m-20 pb-2 text-3xl font-semibold tracking-tight first:mt-0 self-center flex items-center gap-4">
        Gastos asociados
        <DialogUpdateExpense petId={id} create={true} />
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
                <div>
                  <Button
                    onClick={() => deleteExpense(expense.id)}
                    isLoading={deleteLoading}
                    disabled={deleteLoading}
                  >
                    <FaTrash color="red" />
                  </Button>

                  <DialogUpdateExpense
                    petId={id}
                    existingData={expense}
                    expenseId={expense.id}
                  />
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
