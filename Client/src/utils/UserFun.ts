// Checks whether the entered date (date of birth)
// corresponds to the day and month of the current day
// (i.e., whether today is the birthday).

export const isBirthday = (birthdayDate: Date | string): boolean => {

  const validBirthdayDate = new Date(birthdayDate);

  if (isNaN(validBirthdayDate.getTime())) {
    return false;
  }

  const today = new Date();

  return (
    today.getDate() === validBirthdayDate.getDate() &&
    today.getMonth() === validBirthdayDate.getMonth()
  );
};

// Formats a date in a readable and local format in the Hebrew language.

export const formatDate = (date: string): string => {
  return new Date(date).toLocaleDateString("he-IL", {
    year: "numeric",
    month: "long",
    day: "numeric",
  });
};

// Formats a number as currency in Israeli Shekel (ILS) in local format.
export const formatCurrency = (amount: number): string => {
  return new Intl.NumberFormat("he-IL", {
    style: "currency",
    currency: "ILS",
  }).format(amount);
};
