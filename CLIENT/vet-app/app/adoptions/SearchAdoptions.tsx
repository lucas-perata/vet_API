"use client";
import React from "react";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { Button } from "@/components/ui/button";
import { Form } from "@/components/ui/form";
import { toast } from "@/components/ui/use-toast";
import { SelectAdoptions } from "@/components/forms/SelectAdoptions";

const FormSchema = z.object({
  especie: z.string({
    required_error: "Seleccioná una especie",
  }),
});

function SearchAdoptions() {
  const form = useForm<z.infer<typeof FormSchema>>({
    resolver: zodResolver(FormSchema),
  });

  function onSubmit(data: z.infer<typeof FormSchema>) {
    toast({
      title: "You submitted the following values:",
      description: (
        <pre className="mt-2 w-[340px] rounded-md bg-slate-950 p-4">
          <code className="text-white">{JSON.stringify(data, null, 2)}</code>
        </pre>
      ),
    });
  }
  return (
    <div className="flex flex-col border shadow-xl rounded-xl w-10/12">
      <div className="self-center p-4">
        <p className="text-xl">Mascotas en adopción</p>
      </div>

      <Form {...form}>
        <form
          onSubmit={form.handleSubmit(onSubmit)}
          className="w-full h-[15vh] p-5 flex justify-around"
        >
          <SelectAdoptions name="especie" label="especie" form={form} />
          <SelectAdoptions name="especie" label="especie" form={form} />
          <SelectAdoptions name="especie" label="especie" form={form} />

          <div className="self-center">
            <Button className="" type="submit">
              Filtrar
            </Button>
          </div>
        </form>
      </Form>
    </div>
  );
}

export default SearchAdoptions;
