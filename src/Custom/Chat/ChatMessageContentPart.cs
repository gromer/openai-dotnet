using System;
using System.Collections.Generic;

namespace OpenAI.Chat;

/// <summary>
///     A part of the chat message content.
///     <list>
///         <item>
///             Call <see cref="CreateTextPart(string)"/> to create a <see cref="ChatMessageContentPart"/> that
///             encapsulates text.
///         </item>
///         <item>
///             Call <see cref="CreateImagePart(Uri, ChatImageDetailLevel?)"/> or
///             <see cref="CreateImagePart(BinaryData, string, OpenAI.Chat.ChatImageDetailLevel?)"/> to create a
///             <see cref="ChatMessageContentPart"/> that encapsulates an image.
///         </item>
///         <item>
///             Call <see cref="CreateRefusalPart(string)"/> to create a <see cref="ChatMessageContentPart"/> that
///             encapsulates a refusal coming from the model.
///         </item>
///     </list>
/// </summary>
[CodeGenModel("ChatMessageContentPart")]
[CodeGenSuppress("ChatMessageContentPart", typeof(IDictionary<string, BinaryData>))]
public partial class ChatMessageContentPart
{
    private readonly ChatMessageContentPartKind _kind;
    private readonly string _text;
    private readonly InternalChatCompletionRequestMessageContentPartImageImageUrl _imageUri;
    private readonly string _refusal;

    // CUSTOM: Made internal.
    internal ChatMessageContentPart()
    {
    }

    // CUSTOM: Added to support deserialization.
    internal ChatMessageContentPart(ChatMessageContentPartKind kind, string text, InternalChatCompletionRequestMessageContentPartImageImageUrl imageUri, string refusal, IDictionary<string, BinaryData> serializedAdditionalRawData)
    {
        _kind = kind;
        _text = text;
        _imageUri = imageUri;
        _refusal = refusal;
        SerializedAdditionalRawData = serializedAdditionalRawData;
    }

    /// <summary> The kind of content part. </summary>
    public ChatMessageContentPartKind Kind => _kind;

    // CUSTOM: Spread.
    /// <summary> The text. </summary>
    /// <remarks> Present when <see cref="Kind"/> is <see cref="ChatMessageContentPartKind.Text"/>. </remarks>
    public string Text => _text;

    // CUSTOM: Spread.
    /// <summary> The public internet URI where the image is located. </summary>
    /// <remarks> Present when <see cref="Kind"/> is <see cref="ChatMessageContentPartKind.Image"/>. </remarks>
    public Uri ImageUri => _imageUri?.ImageUri;

    // CUSTOM: Spread.
    /// <summary> The image bytes. </summary>
    /// <remarks> Present when <see cref="Kind"/> is <see cref="ChatMessageContentPartKind.Image"/>. </remarks>
    public BinaryData ImageBytes => _imageUri?.ImageBytes;

    // CUSTOM: Spread.
    /// <summary> The MIME type of the image, e.g., <c>image/png</c>. </summary>
    /// <remarks> Present when <see cref="Kind"/> is <see cref="ChatMessageContentPartKind.Image"/>. </remarks>
    public string ImageBytesMediaType => _imageUri?.ImageBytesMediaType;

    // CUSTOM: Spread.
    /// <summary>
    ///     The level of detail with which the model should process the image and generate its textual understanding of
    ///     it. Learn more in the <see href="https://platform.openai.com/docs/guides/vision/low-or-high-fidelity-image-understanding">vision guide</see>.
    /// </summary>
    /// <remarks> Present when <see cref="Kind"/> is <see cref="ChatMessageContentPartKind.Image"/>. </remarks>
    public ChatImageDetailLevel? ImageDetailLevel => _imageUri?.Detail;

    // CUSTOM: Spread.
    /// <summary> The refusal message generated by the model. </summary>
    /// <remarks> Present when <see cref="Kind"/> is <see cref="ChatMessageContentPartKind.Refusal"/>. </remarks>
    public string Refusal => _refusal;

    /// <summary> Creates a new <see cref="ChatMessageContentPart"/> that encapsulates text. </summary>
    /// <param name="text"> The text. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="text"/> is null. </exception>
    public static ChatMessageContentPart CreateTextPart(string text)
    {
        Argument.AssertNotNull(text, nameof(text));

        return new ChatMessageContentPart(
            kind: ChatMessageContentPartKind.Text,
            text: text,
            imageUri: null,
            refusal: null,
            serializedAdditionalRawData: null);
    }

    /// <summary> Creates a new <see cref="ChatMessageContentPart"/> that encapsulates an image. </summary>
    /// <param name="imageUri"> The public internet URI where the image is located. </param>
    /// <param name="imageDetailLevel">
    ///     The level of detail with which the model should process the image and generate its textual understanding of
    ///     it. Learn more in the <see href="https://platform.openai.com/docs/guides/vision/low-or-high-fidelity-image-understanding">vision guide</see>.
    /// </param>
    /// <exception cref="ArgumentNullException"> <paramref name="imageUri"/> is null. </exception>
    public static ChatMessageContentPart CreateImagePart(Uri imageUri, ChatImageDetailLevel? imageDetailLevel = null)
    {
        Argument.AssertNotNull(imageUri, nameof(imageUri));

        return new ChatMessageContentPart(
            kind: ChatMessageContentPartKind.Image,
            text: null,
            imageUri: new(imageUri) { Detail = imageDetailLevel },
            refusal: null,
            serializedAdditionalRawData: null);
    }

    /// <summary> Creates a new <see cref="ChatMessageContentPart"/> that encapsulates an image. </summary>
    /// <param name="imageBytes"> The image bytes. </param>
    /// <param name="imageBytesMediaType"> The MIME type of the image, e.g., <c>image/png</c>. </param>
    /// <param name="imageDetailLevel">
    ///     The level of detail with which the model should process the image and generate its textual understanding of
    ///     it. Learn more in the <see href="https://platform.openai.com/docs/guides/vision/low-or-high-fidelity-image-understanding">vision guide</see>.
    /// </param>
    /// <exception cref="ArgumentNullException"> <paramref name="imageBytes"/> or <paramref name="imageBytesMediaType"/> is null. </exception>
    /// <exception cref="ArgumentException"> <paramref name="imageBytesMediaType"/> is an empty string, and was expected to be non-empty. </exception>
    public static ChatMessageContentPart CreateImagePart(BinaryData imageBytes, string imageBytesMediaType, ChatImageDetailLevel? imageDetailLevel = null)
    {
        Argument.AssertNotNull(imageBytes, nameof(imageBytes));
        Argument.AssertNotNullOrEmpty(imageBytesMediaType, nameof(imageBytesMediaType));

        return new ChatMessageContentPart(
            kind: ChatMessageContentPartKind.Image,
            text: null,
            imageUri: new(imageBytes, imageBytesMediaType) { Detail = imageDetailLevel },
            refusal: null,
            serializedAdditionalRawData: null);
    }

    /// <summary> Creates a new <see cref="ChatMessageContentPart"/> that encapsulates a refusal coming from the model. </summary>
    /// <param name="refusal"> The refusal message generated by the model. </param>
    /// <exception cref="ArgumentNullException"> <paramref name="refusal"/> is null. </exception>
    public static ChatMessageContentPart CreateRefusalPart(string refusal)
    {
        Argument.AssertNotNull(refusal, nameof(refusal));

        return new ChatMessageContentPart(
            kind: ChatMessageContentPartKind.Refusal,
            text: null,
            imageUri: null,
            refusal: refusal,
            serializedAdditionalRawData: null);
    }

    /// <summary>
    ///     Implicitly instantiates a new <see cref="ChatMessageContentPart"/> from a <see cref="string"/>. As such,
    ///     using a <see cref="string"/> in place of a <see cref="ChatMessageContentPart"/> is equivalent to calling the
    ///     <see cref="CreateTextPart(string)"/> method.
    /// </summary>
    /// <param name="text"> The text encapsulated by this <see cref="ChatMessageContentPart"/>. </param>
    public static implicit operator ChatMessageContentPart(string text) => CreateTextPart(text);
}
