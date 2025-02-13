// The IDashboardProps interface responsible for Structure declaration
// for FormInputField Props
export interface IFormInputField {
    label: string;
    type: string;
    placeholder: string;
    register: (name: string, options?: Record<string, unknown>) => unknown;
    validation?: Record<string, unknown>;
    error?: { message: string };
  }