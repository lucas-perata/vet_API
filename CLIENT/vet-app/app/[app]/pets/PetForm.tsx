"use client";
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { format } from "date-fns";
import { Checkbox } from "@/components/ui/checkbox";
import {
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
} from "@/components/ui/command";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { Calendar } from "@/components/ui/calendar";
import { LuCake } from "react-icons/lu";
import { CaretSortIcon, CheckIcon } from "@radix-ui/react-icons";
import { zodResolver } from "@hookform/resolvers/zod";
import { cn } from "@/lib/utils";
import React from "react";
import { useForm } from "react-hook-form";
import { z } from "zod";
import InputForm from "@/components/forms/Input";
import { useRouter } from "next/navigation";
import { createPet } from "@/app/actions/petActionsCS";

export default function PetForm() {
  const breeds = [
    { label: "Labrador", value: "1" },
    { label: "Ovejero Alemán", value: "2" },
    { label: "Sin raza", value: "3" },
  ] as const;
  const {
    register,
    handleSubmit,
    setFocus,
    formState: { isSubmitting, isValid, isDirty, errors },
  } = useForm();

  const router = useRouter();

  const formSchema = z.object({
    name: z.string().min(2).max(15),
    weight: z.coerce.number().min(0.1).max(60),
    breed: z.string({ required_error: "Selecciona una raza" }),
    color: z.string().min(2).max(20),
    gender: z.string().min(2).max(10),
    isNeutered: z.boolean().default(false),
    dateOfBirth: z.date({
      required_error: "Es necesario agregar la fecha de nacimiento.",
    }),
  });

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
  });

  function onSubmit(data: z.infer<typeof formSchema>) {
    createPet(data).then((response) => {
      router.push(`/app/pets/details/${response.id}`);
    });
  }

  return (
    <Form {...form}>
      <form
        className="flex flex-col mt-3 gap-4"
        onSubmit={form.handleSubmit(onSubmit)}
      >
        <InputForm form={form} label="Name" name="name" />
        <InputForm form={form} label="Gender" name="gender" />
        <div className="flex gap-4">
          <div className="mb-3 block basis-1/3">
            <FormField
              control={form.control}
              name="breed"
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
                            ? breeds.find(
                              (breed) => breed.value === field.value,
                            )?.label
                            : "Selecciona la raza"}
                          <CaretSortIcon className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                        </Button>
                      </FormControl>
                    </PopoverTrigger>
                    <PopoverContent className="w-[200px] p-0">
                      <Command>
                        <CommandInput
                          placeholder="Buscar raza..."
                          className="h-9"
                        />
                        <CommandEmpty>No se encontró una raza</CommandEmpty>
                        <CommandGroup>
                          {breeds.map((breed) => (
                            <CommandItem
                              value={breed.label}
                              key={breed.value}
                              onSelect={() => {
                                form.setValue("breed", breed.value);
                              }}
                            >
                              {breed.label}
                              <CheckIcon
                                className={cn(
                                  "ml-auto h-4 w-4",
                                  breed.value === field.value
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
          <div className="mb-3 block basis-1/3">
            <InputForm form={form} label="Color" name="color" />
          </div>
          <div className="mb-3 block basis-1/3">
            <InputForm form={form} label="Peso" name="weight" type="number" />
          </div>
        </div>
        <div className="flex gap-4">
          <div>
            <FormField
              control={form.control}
              name="dateOfBirth"
              render={({ field }) => (
                <FormItem className="flex flex-col">
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
                            <span>Fecha de nacimiento</span>
                          )}
                          <LuCake className="ml-auto h-4 w-4 opacity-50" />
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

                  <FormMessage />
                </FormItem>
              )}
            />
          </div>
          <div className="flex basis-1/2">
            <InputForm label="photo" name="photo" type="file" form={form} />
          </div>
        </div>
        <div>
          <FormField
            control={form.control}
            name="isNeutered"
            render={({ field }) => (
              <FormItem className="flex flex-row items-start space-x-3 space-y-0 rounded-md p-4">
                <FormControl>
                  <Checkbox
                    checked={field.value}
                    onCheckedChange={field.onChange}
                  />
                </FormControl>
                <div className="space-y-1 leading-none">
                  <FormLabel>¿Está castrada?</FormLabel>
                </div>
              </FormItem>
            )}
          />
        </div>
        <div className="flex gap-4 justify-end">
          <Button className="bg-red-500">Cancelar</Button>
          <Button disabled={!isValid} type="submit">
            Crear
          </Button>
        </div>
      </form>
    </Form>
  );
}
