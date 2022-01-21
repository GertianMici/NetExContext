using Microsoft.EntityFrameworkCore;
using Moq;
using NetExContexts.Shared.Brokers;
using NetExContexts.Shared.Models.Exceptions;
using NetExContexts.Shared.Services;
using Npgsql;
using System;
using System.Runtime.Serialization;
using Tynamix.ObjectFiller;
using Xunit;

namespace NetExContexts.Tests.Services
{
    public class NetExContextServiceTests
    {
        private readonly Mock<IDbErrorBroker> dbErrorBrokerMock;
        private readonly INetExContextService netExContextService;

        public NetExContextServiceTests()
        {
            this.dbErrorBrokerMock = new Mock<IDbErrorBroker>();
            this.netExContextService = new NetExContextService(this.dbErrorBrokerMock.Object);
        }

        [Fact]
        public void ShouldThrowDbUpdateExceptionIfErrorCodeIsNotRecognized()
        {
            //given
            int npgsqlForeignKeyConstraintConflictErrorCode = 0000;
            string randomErrorMessage = CreateRandonErrorMessage();
            PostgresException foreignKeyConstraintConflictException = CreateNpgsqlException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: foreignKeyConstraintConflictException);

            dbErrorBrokerMock.Setup(broker =>
                broker.GetDbErrorCode(foreignKeyConstraintConflictException))
                .Returns(npgsqlForeignKeyConstraintConflictErrorCode);

            //when. then
            Assert.Throws<DbUpdateException>(() =>
                netExContextService.ThrowCustomException(dbUpdateException));
        }

        [Fact]
        public void ShouldThrowInvalidColumnNameException()
        {
            //given
            int npgsqlInvalidColumnNameErrorCode = 207;
            string randomErrorMessage = CreateRandonErrorMessage();
            PostgresException invalidColumnNameException = CreateNpgsqlException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: invalidColumnNameException);

            dbErrorBrokerMock.Setup(broker =>
            broker.GetDbErrorCode(invalidColumnNameException))
                .Returns(npgsqlInvalidColumnNameErrorCode);

            //when. then
            Assert.Throws<InvalidColumnNameException>(() =>
                netExContextService.ThrowCustomException(dbUpdateException));
        }

        [Fact]
        public void ShouldThrowInvalidObjectNameException()
        {
            //given
            int npgsqlInvalidObjectNameErrorCode = 208;
            string randomErrorMessage = CreateRandonErrorMessage();
            PostgresException invalidObjectNameException = CreateNpgsqlException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: invalidObjectNameException);

            dbErrorBrokerMock.Setup(broker =>
                broker.GetDbErrorCode(invalidObjectNameException))
                .Returns(npgsqlInvalidObjectNameErrorCode);

            //when. then
            Assert.Throws<InvalidObjectNameException>(() =>
                netExContextService.ThrowCustomException(dbUpdateException));
        }
        public void ShouldThrowForeignKeyConstraintConflictException()
        {
            // given
            int npgsqlForeignKeyConstraintConflictErrorCode = 547;
            string randomErrorMessage = CreateRandonErrorMessage();
            PostgresException foreignKeyConstraintConflictException = CreateNpgsqlException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: foreignKeyConstraintConflictException);

            dbErrorBrokerMock.Setup(broker =>
                broker.GetDbErrorCode(foreignKeyConstraintConflictException))
                    .Returns(npgsqlForeignKeyConstraintConflictErrorCode);

            // when . then
            Assert.Throws<ForeignKeyConstraintConflictException>(() =>
                netExContextService.ThrowCustomException(dbUpdateException));
        }

        [Fact]
        public void ShouldThrowDuplicateKeyWithUniqueIndexException()
        {
            // given
            int npgsqlDuplicateKeyErrorCode = 2601;
            string randomErrorMessage = CreateRandonErrorMessage();
            PostgresException duplicateKeyException = CreateNpgsqlException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: duplicateKeyException);

            dbErrorBrokerMock.Setup(broker =>
                broker.GetDbErrorCode(duplicateKeyException))
                    .Returns(npgsqlDuplicateKeyErrorCode);

            // when . then
            Assert.Throws<DuplicateKeyWithUniqueIndexException>(() =>
                netExContextService.ThrowCustomException(dbUpdateException));
        }

        [Fact]
        public void ShouldThrowDuplicateKeyException()
        {
            // given
            int npgsqlDuplicateKeyErrorCode = 2627;
            string randomErrorMessage = CreateRandonErrorMessage();
            PostgresException duplicateKeyException = CreateNpgsqlException();

            var dbUpdateException = new DbUpdateException(
                message: randomErrorMessage,
                innerException: duplicateKeyException);

            dbErrorBrokerMock.Setup(broker =>
                broker.GetDbErrorCode(duplicateKeyException))
                    .Returns(npgsqlDuplicateKeyErrorCode);

            // when . then
            Assert.Throws<DuplicateKeyException>(() =>
                netExContextService.ThrowCustomException(dbUpdateException));
        }

        [Fact]
        public void ShouldThrowDbUpdateExceptionIfSqlExceptionWasNull()
        {
            // given
            var dbUpdateException = new DbUpdateException(null, default(Exception));

            // when . then
            Assert.Throws<DbUpdateException>(() =>
                netExContextService.ThrowCustomException(dbUpdateException));

            dbErrorBrokerMock.Verify(broker =>
                broker.GetDbErrorCode(It.IsAny<PostgresException>()),
                    Times.Never);
        }
        private static PostgresException CreateNpgsqlException() =>
            FormatterServices.GetUninitializedObject(typeof(PostgresException)) as PostgresException;

        private static string CreateRandonErrorMessage() => new MnemonicString().GetValue();
    }
}
