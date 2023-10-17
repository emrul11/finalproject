
        $(document).ready(function () {
            var toast = $('.toast');
        toast.toast('show');

        // Close the toast when the close button is clicked
        toast.find('.btn-close').on('click', function () {
            toast.toast('hide');
            });

        // Show the delete confirmation modal when a delete button is clicked
        $('.show-bs-modal').on('click', function () {
                var deleteForm = $(this).closest('form');
        $('#deleteModal').modal('show');

        // When the user confirms deletion, submit the form
        $('#confirmDelete').on('click', function () {
            deleteForm.submit();
                });

            });
        });
        // Handle the "No" button click event
        $('#noButton').on('click', function () {
            
            $('#deleteModal').modal('hide'); // Close the modal
        });

    