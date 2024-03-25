"use client";
import React from "react";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { Button } from "@/components/ui/button";
import { Form } from "@/components/ui/form";
import { toast } from "@/components/ui/use-toast";
import { SelectAdoptions } from "@/components/forms/SelectAdoptions";
import { createInstance } from "@/utils/axiosConfig";
import useStore from "@/store/store";
import { useFilterAdoptions } from "../hooks/useAdoptions";

const FormSchema = z.object({
  gender: z.string().optional(),
  area: z.string().optional(),
  province: z.string().optional(),
});

function SearchAdoptions() {
  const token = useStore((state) => state.token);
  const axiosI = createInstance(token());

  const { filterData } = useFilterAdoptions(axiosI);
  const form = useForm<z.infer<typeof FormSchema>>({
    resolver: zodResolver(FormSchema),
  });

  function onSubmit(data: z.infer<typeof FormSchema>) {
    filterData(data.area);
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
          <SelectAdoptions name="gender" label="género" form={form} />
          <SelectAdoptions name="area" label="area" form={form} />
          <SelectAdoptions name="province" label="Provincia" form={form} />

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
