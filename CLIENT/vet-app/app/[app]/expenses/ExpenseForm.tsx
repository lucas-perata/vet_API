"use client";
import { zodResolver } from "@hookform/resolvers/zod";
import React, { useEffect } from "react";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { useForm } from "react-hook-form";
import { z } from "zod";
import InputForm from "@/components/forms/Input";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popoverDialog";

import { format } from "date-fns";
import { cn } from "@/lib/utils";
import { Calendar } from "@/components/ui/calendar";
import { Button } from "@/components/ui/button";
import {
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
} from "@/components/ui/command";
import { CaretSortIcon, CheckIcon } from "@radix-ui/react-icons";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import useStore from "@/store/store";
import { createInstance } from "@/utils/axiosConfig";
import { useToast } from "@/components/ui/use-toast";

type Props = {
  petId: number;
};

export default function ExpenseForm({ petId }: Props) {
  const { toast } = useToast();

  const token = useStore((state) => state.token);
  const axiosI = createInstance(token());

  const queryClient = useQueryClient();

  const categories = [
    { label: "Labrador", value: "Food" },
    { label: "Ovejero Alemán", value: "2" },
    { label: "Sin raza", value: "3" },
  ] as const;

  const {
    register,
    handleSubmit,
    setFocus,
    formState: { isSubmitting, isValid, isDirty, errors },
  } = useForm();

  const formSchema = z.object({
    category: z.string().min(1).max(15),
    amount: z.coerce.number().min(1).max(100000),
    description: z.string().min(2).max(100),
    date: z.date({ required_error: "Es necesario agregar una fecha." }),
    petId: z.coerce.number().min(1),
  });

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
  });

  useEffect(() => {
    form.setValue("petId", petId);
  }, [petId, form]);

  //  function onSubmit(data: z.infer<typeof formSchema>) {
  //   console.log(data);
  //   createExpense(data).then((response) => {
  //     console.log(response.id);
  //   });
  // }

  const submitExpenseMutation = async (data: z.infer<typeof formSchema>) => {
    return await axiosI.post("/api/spending", data);
  };

  const { mutate: submitExpense, isLoading } = useMutation({
    mutationFn: async (data: z.infer<typeof formSchema>) =>
      submitExpenseMutation(data),
    onSuccess: () => {
      toast({ description: "Gasto agregado" });
      queryClient.invalidateQueries(["petExpenses"]);
    },
    onError: () => {
      toast({ description: "Error, intentá de nuevo" });
    },
  });

  const onSubmit = (data: z.infer<typeof formSchema>) => {
    submitExpense(data);
  };

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)}>
        <InputForm form={form} label="description" name="description" />
        <InputForm form={form} label="amount" name="amount" type="number" />
        <FormField
          control={form.control}
          name="petId"
          render={({ field }) => <input {...field} value={petId} readOnly />}
        />
        <div className="flex gap-4">
          <div>
            <FormField
              control={form.control}
              name="date"
              render={({ field }) => (
                <FormItem className="flex flex-col">
                  <div>
                    <Popover>
                      <PopoverTrigger asChild>
                        <FormControl>
                          <Button
                            variant={"outline"}
                            className={cn(
                              "w-[240px] pl-3 text-left font-normal",
                              !field.value && "text-muted-foreground",
                            )}
                          >
                            {field.value ? (
                              format(field.value, "PPP")
                            ) : (
                              <span>Fecha</span>
                            )}
                          </Button>
                        </FormControl>
                      </PopoverTrigger>
                      <PopoverContent className="w-auto p-0" align="start">
                        <Calendar
                          mode="single"
                          selected={field.value}
                          onSelect={field.onChange}
                          disabled={(date) =>
                            date > new Date() || date < new Date("1900-01-01")
                          }
                          initialFocus
                        />
                      </PopoverContent>
                    </Popover>
                  </div>

                  <FormMessage />
                </FormItem>
              )}
            />
          </div>
          <div>
            <div className="mb-3 block basis-1/3"></div>
          </div>
        </div>
        <div>
          <FormField
            control={form.control}
            name="category"
            render={({ field }) => (
              <FormItem className="">
                <Popover>
                  <PopoverTrigger asChild>
                    <FormControl>
                      <Button
                        variant="outline"
                        role="combobox"
                        className={cn(
                          "w-[400px] justify-between",
                          !field.value && "text-muted-foreground",
                        )}
                      >
                        {field.value
                          ? categories.find(
                            (category) => category.value === field.value,
                          )?.label
                          : "Selecciona la categoría"}
                        <CaretSortIcon className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                      </Button>
                    </FormControl>
                  </PopoverTrigger>
                  <PopoverContent className="w-[200px] p-0">
                    <Command>
                      <CommandInput
                        placeholder="Seleccionar categoría"
                        className="h-9"
                      />
                      <CommandEmpty>No se encontró categoría</CommandEmpty>
                      <CommandGroup>
                        {categories.map((category) => (
                          <CommandItem
                            value={category.label}
                            key={category.value}
                            onSelect={() => {
                              form.setValue("category", category.value);
                            }}
                          >
                            {category.label}
                            <CheckIcon
                              className={cn(
                                "ml-auto h-4 w-4",
                                category.value === field.value
                                  ? "opacity-100"
                                  : "opacity-0",
                              )}
                            />
                          </CommandItem>
                        ))}
                      </CommandGroup>
                    </Command>
                  </PopoverContent>
                </Popover>
                <FormMessage />
              </FormItem>
            )}
          />
        </div>
        <div>
          <Button type="submit">Crear</Button>
        </div>
      </form>
    </Form>
  );
}
