const rules = {
  taskName: [
    { required: true, message: "Please input task name", trigger: "blur" }
  ],
  date: [
    {
      type: "date",
      required: true,
      message: "Please pick a date",
      trigger: "change"
    }
  ],
  roomies: [
    {
      type: "array",
      required: true,
      message: "Please assign at least one roomie",
      trigger: "change"
    }
  ]
};

export default rules;
