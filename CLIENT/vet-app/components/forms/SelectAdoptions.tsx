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
  selection: {}[];
  onValueChange?: (value: string) => void;
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
            <Select
              onValueChange={(value) => {
                field.onChange(value);
                props.onValueChange?.(value);
              }}
              defaultValue={field.value}
            >
              <FormControl>
                <SelectTrigger>
                  <SelectValue placeholder="" />
                </SelectTrigger>
              </FormControl>
              <SelectContent>
                {props.selection.map((value) => (
                  <SelectItem key={value.name} value={value.name}>
                    {value.name}
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
            <FormMessage />
          </FormItem>
        )}
      />
    </div>
  );
};
