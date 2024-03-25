import React from "react";
import {
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { UseControllerProps } from "react-hook-form";

type Props = {
  label: string;
  name: string;
  form: any;
} & UseControllerProps;

export const SelectAdoptions = (props: Props) => {
  return (
    <div>
      <FormField
        control={props.form.control}
        name={props.name}
        render={({ field }) => (
          <FormItem className="w-52">
            <FormLabel>{props.label}</FormLabel>
            <Select onValueChange={field.onChange} defaultValue={field.value}>
              <FormControl>
                <SelectTrigger>
                  <SelectValue placeholder="Seleccioná una especie" />
                </SelectTrigger>
              </FormControl>
              <SelectContent>
                <SelectItem value="Morón">Moron</SelectItem>
                <SelectItem value="string">string</SelectItem>
                <SelectItem value="m@support.com">m@support.com</SelectItem>
              </SelectContent>
            </Select>

            <FormMessage />
          </FormItem>
        )}
      />
    </div>
  );
};
